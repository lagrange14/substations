using Robust.Shared.Serialization;

namespace Content.Shared.Atmos.Piping.Unary.Components;

public sealed partial class GasVentPumpData
{
    public VentPumpFlowmos VentFlowmosMode { get; set; } = VentPumpFlowmos.Default;

    public static GasVentPumpData FilterInletPreset = new()
    {
        Enabled = true,
        PumpDirection = VentPumpDirection.Releasing,
        PressureChecks = VentPressureBound.ExternalBound,
        VentFlowmosMode = VentPumpFlowmos.Inlet,
        ExternalPressureBound = Atmospherics.OneAtmosphere + Atmospherics.AirVentPressureDelta,
        InternalPressureBound = 0f,
        PressureLockoutOverride = false,
    };

    public static GasVentPumpData FilterOutletPreset = new()
    {
        Enabled = true,
        PumpDirection = VentPumpDirection.Siphoning,
        PressureChecks = VentPressureBound.ExternalBound,
        VentFlowmosMode = VentPumpFlowmos.Outlet,
        ExternalPressureBound = Atmospherics.OneAtmosphere - Atmospherics.AirVentPressureDelta,
        InternalPressureBound = 0f,
        PressureLockoutOverride = false,
    };

    public static GasVentPumpData FilterWideInletPreset = new()
    {
        Enabled = true,
        PumpDirection = VentPumpDirection.Releasing,
        PressureChecks = VentPressureBound.ExternalBound,
        VentFlowmosMode = VentPumpFlowmos.Inlet,
        ExternalPressureBound = Atmospherics.OneAtmosphere + Atmospherics.AirVentWideFilterPressureDelta,
        InternalPressureBound = 0f,
        PressureLockoutOverride = false,
    };

    public static GasVentPumpData FilterWideOutletPreset = new()
    {
        Enabled = true,
        PumpDirection = VentPumpDirection.Siphoning,
        PressureChecks = VentPressureBound.ExternalBound,
        VentFlowmosMode = VentPumpFlowmos.Outlet,
        ExternalPressureBound = Atmospherics.OneAtmosphere - Atmospherics.AirVentWideFilterPressureDelta,
        InternalPressureBound = 0f,
        PressureLockoutOverride = false,
    };

    // FilterInlet but just disabled
    public static GasVentPumpData PanicModeInletPreset = new()
    {
        Enabled = false,
        PumpDirection = VentPumpDirection.Releasing,
        PressureChecks = VentPressureBound.ExternalBound,
        VentFlowmosMode = VentPumpFlowmos.Inlet,
        ExternalPressureBound = Atmospherics.OneAtmosphere + Atmospherics.AirVentPressureDelta,
        InternalPressureBound = 0f,
        PressureLockoutOverride = false,
    };

    // FilterOutlet but external pressure bound set to 0 kPa
    public static GasVentPumpData PanicModeOutletPreset = new()
    {
        Enabled = true,
        PumpDirection = VentPumpDirection.Siphoning,
        PressureChecks = VentPressureBound.ExternalBound,
        VentFlowmosMode = VentPumpFlowmos.Outlet,
        ExternalPressureBound = 0f,
        InternalPressureBound = 0f,
        PressureLockoutOverride = false,
    };

    // FilterInlet but pressure lockout overriden and max external pressure bound
    public static GasVentPumpData FillModeInletPreset = new()
    {
        Enabled = true,
        PumpDirection = VentPumpDirection.Releasing,
        PressureChecks = VentPressureBound.ExternalBound,
        VentFlowmosMode = VentPumpFlowmos.Inlet,
        ExternalPressureBound = Atmospherics.OneAtmosphere * 50,
        InternalPressureBound = 0f,
        PressureLockoutOverride = true,
    };

    // FilterOutlet but disabled
    public static GasVentPumpData FillModeOutletPreset = new()
    {
        Enabled = false,
        PumpDirection = VentPumpDirection.Siphoning,
        PressureChecks = VentPressureBound.ExternalBound,
        VentFlowmosMode = VentPumpFlowmos.Outlet,
        ExternalPressureBound = Atmospherics.OneAtmosphere - Atmospherics.AirVentPressureDelta,
        InternalPressureBound = 0f,
        PressureLockoutOverride = false,
    };

    [Flags]
    [Serializable, NetSerializable]
    public enum VentPumpFlowmos : sbyte
    {
        Default = 0,
        Inlet = 1, // Inlet is attached distro, outlet is attached to waste
        Outlet = 2,
    }
}
