namespace Content.Shared.Atmos;

public static partial class Atmospherics
{
    /// <summary>
    /// The difference from one atmosphere that inlet and outlet vents should try set their bounds to, in kPa.
    /// This should perhaps be part of the air vent component.
    /// </summary>
    public const float AirVentPressureDelta = 5;

    /// <summary>
    /// The delta that should be used when the wide-filtering alarm preset is activated.
    /// </summary>
    /// <seealso cref="AirVentPressureDelta" />
    public const float AirVentWideFilterPressureDelta = 50;
}
