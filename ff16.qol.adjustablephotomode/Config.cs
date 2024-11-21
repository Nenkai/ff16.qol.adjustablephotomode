using ff16.qol.adjustablephotomode.Template.Configuration;

using Reloaded.Mod.Interfaces.Structs;

using System.ComponentModel;

namespace ff16.qol.adjustablephotomode.Configuration;

public class Config : Configurable<Config>
{
    [DisplayName("Collision Sphere Radius")]
    [Description("Radius of the collision box (originally 2.5)")]
    [DefaultValue(10000.0f)]
    public float CollisionSphereRadius { get; set; } = 10000.0f;

    [DisplayName("Camera Move Speed")]
    [Description("Camera Move Speed (originally 2.5)")]
    [DefaultValue(10.0f)]
    public float CameraMoveSpeed { get; set; } = 10.0f;

    [DisplayName("Camera Up/Down Speed")]
    [Description("Camera Up/Down Speed (originally 0.5)")]
    [DefaultValue(2.0f)]
    public float CameraUpDownSpeed { get; set; } = 2.0f;

    [DisplayName("Camera Rotation Speed")]
    [Description("Camera Rotation Speed (originally 45)")]
    [DefaultValue(45.0f)]
    public float CameraRotationSpeed { get; set; } = 45.0f;

    [DisplayName("Default Field of View (FOV)")]
    [Description("Default Field of View (originally 45)")]
    [DefaultValue(45.0f)]
    public float FieldOfView { get; set; } = 45.0f;

    [DisplayName("Field of View Min (FOV)")]
    [Description("Field of View Minimum (originally 10)")]
    [DefaultValue(10.0f)]
    public float FieldOfViewMin { get; set; } = 10.0f;

    [DisplayName("Field of View Max (FOV)")]
    [Description("Field of View Maximum (originally 80)")]
    [DefaultValue(80.0f)]
    public float FieldOfViewMax { get; set; } = 80f;

    [DisplayName("Default Blur")]
    [Description("Default Blur (originally 1.0)")]
    [DefaultValue(1.0f)]
    public float DefaultBlur { get; set; } = 1.0f;

    [DisplayName("Blur Min")]
    [Description("Blur Minimum (originally 0.1)")]
    [DefaultValue(0.1f)]
    public float BlurMin { get; set; } = 0.1f;

    [DisplayName("Blur Max")]
    [Description("Blur Maximum (originally 10.0)")]
    [DefaultValue(10.0f)]
    public float BlurMax { get; set; } = 10.0f;

    [DisplayName("Default Focal Distance")]
    [Description("Default Focal Distance (originally 2.5)")]
    [DefaultValue(2.5f)]
    public float DefaultFocalDistance { get; set; } = 2.5f;

    [DisplayName("Focal Distance Min")]
    [Description("Focal Distance Minimum (originally 0.0)")]
    [DefaultValue(0f)]
    public float FocalDistanceMin { get; set; } = 0f;

    [DisplayName("Focal Distance Max")]
    [Description("Focal Distance Maximum (originally 30.0)")]
    [DefaultValue(30.0f)]
    public float FocalDistanceMax { get; set; } = 30.0f;

    /*
    [DisplayName("Brightness Min")]
    [Description("Brightness Minimum (originally 0.0)")]
    [DefaultValue(0.3f)]
    public float BrightnessMin { get; set; } = 0.3f;

    [DisplayName("Brightness Max")]
    [Description("Brightness Maximum (originally 3.5)")]
    [DefaultValue(30.0f)]
    public float BrightnessMax { get; set; } = 3.5f;

    [DisplayName("Saturation Min")]
    [Description("Saturation Minimum (originally 0.0)")]
    [DefaultValue(0.3f)]
    public float SaturationMin { get; set; } = 0f;

    [DisplayName("Saturation Max")]
    [Description("Saturation Maximum (originally 10.0)")]
    [DefaultValue(10.0f)]
    public float SaturationMax { get; set; } = 10.0f;
    */
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}
