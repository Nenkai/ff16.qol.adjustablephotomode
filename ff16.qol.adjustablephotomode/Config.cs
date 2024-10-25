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
    [DefaultValue(10.0)]
    public float CameraMoveSpeed { get; set; } = 10.0f;

    [DisplayName("Camera Up/Down Speed")]
    [Description("Camera Up/Down Speed (originally 0.5)")]
    [DefaultValue(2.0f)]
    public float CameraUpDownSpeed { get; set; } = 2.0f;

    [DisplayName("Camera Rotation Speed")]
    [Description("Camera Rotation Speed (originally 45)")]
    [DefaultValue(45.0f)]
    public float CameraRotationSpeed { get; set; } = 45.0f;

    [DisplayName("Field of View (FOV)")]
    [Description("Field of View (originally 45)")]
    [DefaultValue(45.0f)]
    public float FieldOfView { get; set; } = 45.0f;

    [DisplayName("Field of View Min (FOV)")]
    [Description("Field of View Minimum (originally 10)")]
    [DefaultValue(10.0f)]
    public float FieldOfViewMin { get; set; } = 10.0f;

    [DisplayName("Field of View Min (FOV)")]
    [Description("Field of View Minimum (originally 80)")]
    [DefaultValue(80.0f)]
    public float FieldOfViewMax { get; set; } = 80f;
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}
