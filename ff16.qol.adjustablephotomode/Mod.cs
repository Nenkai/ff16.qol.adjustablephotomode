using ff16.qol.adjustablephotomode.Configuration;
using ff16.qol.adjustablephotomode.Template;

using Reloaded.Mod.Interfaces;
using Reloaded.Hooks.Definitions;
using Reloaded.Memory.Sigscan;
using Reloaded.Memory.Sigscan.Definitions.Structs;
using Reloaded.Memory.SigScan.ReloadedII.Interfaces;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

using System.Diagnostics;
using Reloaded.Memory.Interfaces;

using FF16Framework.Interfaces;
using FF16Framework.Interfaces.Nex;
using FF16Framework.Interfaces.Nex.Structures;
using FF16Tools.Files;
using FF16Tools.Files.Nex;
using FF16Tools.Files.Nex.Entities;
using Syroot.BinaryData.Memory;

using ff16.utility.modloader;
using ff16.utility.modloader.Interfaces;
using System.Runtime.InteropServices;

namespace ff16.qol.adjustablephotomode;

/// <summary>
/// Your mod logic goes here.
/// </summary>
public class Mod : ModBase // <= Do not Remove.
{
    /// <summary>
    /// Provides access to the mod loader API.
    /// </summary>
    private readonly IModLoader _modLoader;

    /// <summary>
    /// Provides access to the Reloaded.Hooks API.
    /// </summary>
    /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
    private readonly IReloadedHooks? _hooks;

    /// <summary>
    /// Provides access to the Reloaded logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Entry point into the mod, instance that created this class.
    /// </summary>
    private readonly IMod _owner;

    /// <summary>
    /// Provides access to this mod's configuration.
    /// </summary>
    private Config _configuration;

    /// <summary>
    /// The configuration of the currently executing mod.
    /// </summary>
    private readonly IModConfig _modConfig;

    private static IStartupScanner? _startupScanner = null!;

    public WeakReference<INextExcelDBApi> _nexApi;

    public Mod(ModContext context)
    {
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;

#if DEBUG
        Debugger.Launch();
#endif

        _logger.WriteLine($"[{_modConfig.ModId}] Initializing..", _logger.ColorGreen);

        _nexApi = _modLoader.GetController<INextExcelDBApi>();
        if (!_nexApi.TryGetTarget(out INextExcelDBApi nextExcelDBApi))
        {
            _logger.WriteLine($"[{_modConfig.ModId}] Could not get INextExcelDBApi. Is the FFXVI Mod Framework installed/loaded?");
            return;
        }

        nextExcelDBApi.OnNexLoaded += NextExcelDBApi_OnNexLoaded;
    }

    /// <summary>
    /// Fired when the game has loaded all nex tables.
    /// </summary>
    private unsafe void NextExcelDBApi_OnNexLoaded()
    {
        ApplyPhotoCameraParamChanges();
    }

    private unsafe void ApplyPhotoCameraParamChanges()
    {
#if DEBUG
        Debugger.Launch();
#endif

        if (!_nexApi.TryGetTarget(out var nextExcelDBApi))
        {
            _logger.WriteLine($"[{_modConfig.ModId}] Could not get INextExcelDBApi. Is the FFXVI Mod Framework installed/loaded?", _logger.ColorRed);
            return;
        }

        if (!nextExcelDBApi.Initialized)
        {
            _logger.WriteLine($"[{_modConfig.ModId}] Changes NOT applied, the game hasn't initialized the nex database yet.", _logger.ColorYellow);
            return;
        }

        NexTableInstance* photoTable = nextExcelDBApi.GetTable(NexTableIds.photocameraparam);
        if (photoTable is null)
        {
            _logger.WriteLine($"[{_modConfig.ModId}] Unable to fetch table 'photocameraparam'", _logger.ColorRed);
            return;
        }

        
        NexTableLayout layout = TableMappingReader.ReadTableLayout("photocameraparam", new Version(1, 0, 0));
        for (int i = 0; i < photoTable->NumRows; i++)
        {
            NexRowInstance* rowInstance = nextExcelDBApi.SearchRow(photoTable, i);
            if (rowInstance is null)
            {
                _logger.WriteLine($"[{_modConfig.ModId}] Could not get photocameraparam row {i}, skipping", _logger.ColorRed);
                continue;
            }

            Nex1KRowInfo* rowInfo = (Nex1KRowInfo*)rowInstance->RowInfo;
            if (rowInfo->Key1 >= 3)
                continue;

            byte* rowData = nextExcelDBApi.GetRowData(rowInstance);
            *(float*)&rowData[layout.Columns["CollisionSphereRadius"].Offset] = _configuration.CollisionSphereRadius;
            *(float*)&rowData[layout.Columns["CameraMoveSpeed"].Offset] = _configuration.CameraMoveSpeed;
            *(float*)&rowData[layout.Columns["CameraUpDownSpeed"].Offset] = _configuration.CameraUpDownSpeed;
            *(float*)&rowData[layout.Columns["CameraRotationSpeed"].Offset] = _configuration.CameraRotationSpeed;
            *(float*)&rowData[layout.Columns["FieldOfView"].Offset] = _configuration.FieldOfView;
            *(float*)&rowData[layout.Columns["FieldOfViewMin"].Offset] = _configuration.FieldOfViewMin;
            *(float*)&rowData[layout.Columns["FieldOfViewMax"].Offset] = _configuration.FieldOfViewMax;
            *(float*)&rowData[layout.Columns["Blur"].Offset] = _configuration.DefaultBlur;
            *(float*)&rowData[layout.Columns["BlurMin"].Offset] = _configuration.BlurMin;
            *(float*)&rowData[layout.Columns["BlurMax"].Offset] = _configuration.BlurMax;
            *(float*)&rowData[layout.Columns["FocalDistance"].Offset] = _configuration.DefaultFocalDistance;
            *(float*)&rowData[layout.Columns["FocalDistanceMin"].Offset] = _configuration.FocalDistanceMin;
            *(float*)&rowData[layout.Columns["FocalDistanceMax"].Offset] = _configuration.FocalDistanceMax;
        }

        _logger.WriteLine($"[{_modConfig.ModId}] Applied photocameraparam changes.", _logger.ColorGreen);
    }

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        // Apply settings from configuration.
        // ... your code here.
        _configuration = configuration;
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");

        ApplyPhotoCameraParamChanges();
    }
    #endregion

    #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
}