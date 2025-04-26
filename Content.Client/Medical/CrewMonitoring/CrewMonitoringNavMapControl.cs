using Content.Client.Pinpointer.UI;
using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Timing;

namespace Content.Client.Medical.CrewMonitoring;

public sealed partial class CrewMonitoringNavMapControl : NavMapControl
{
    [Dependency] private readonly IEntitySystemManager _entitySystem = default!; // L5 - map-global suit sensors

    public NetEntity? Focus;
    public Dictionary<NetEntity, string> LocalizedNames = new();

    private SharedTransformSystem _transformSystem; // L5 - map-global suit sensors
    private Label _trackedEntityLabel;
    private PanelContainer _trackedEntityPanel;

    public CrewMonitoringNavMapControl() : base()
    {
        // L5 - map-global suit sensors:
        IoCManager.InjectDependencies(this);
        _transformSystem = _entitySystem.GetEntitySystem<SharedTransformSystem>();

        WallColor = new Color(192, 122, 196);
        TileColor = new(71, 42, 72);
        BackgroundColor = Color.FromSrgb(TileColor.WithAlpha(BackgroundOpacity));

        _trackedEntityLabel = new Label
        {
            Margin = new Thickness(10f, 8f),
            HorizontalAlignment = HAlignment.Center,
            VerticalAlignment = VAlignment.Center,
            Modulate = Color.White,
        };

        _trackedEntityPanel = new PanelContainer
        {
            PanelOverride = new StyleBoxFlat
            {
                BackgroundColor = BackgroundColor,
            },

            Margin = new Thickness(5f, 10f),
            HorizontalAlignment = HAlignment.Left,
            VerticalAlignment = VAlignment.Bottom,
            Visible = false,
        };

        _trackedEntityPanel.AddChild(_trackedEntityLabel);
        this.AddChild(_trackedEntityPanel);
    }

    protected override void FrameUpdate(FrameEventArgs args)
    {
        base.FrameUpdate(args);

        if (Focus == null)
        {
            _trackedEntityLabel.Text = string.Empty;
            _trackedEntityPanel.Visible = false;

            return;
        }

        foreach ((var netEntity, var blip) in TrackedEntities)
        {
            if (netEntity != Focus)
                continue;

            if (!LocalizedNames.TryGetValue(netEntity, out var name))
                name = "Unknown";

            // L5 - map-global suit sensors:
            var (gridX, gridY) = (MathF.Round(blip.Coordinates.X), MathF.Round(blip.Coordinates.Y));
            var (mapX, mapY) = _transformSystem.ToMapCoordinates(blip.Coordinates);
            (mapX, mapY) = (MathF.Round(mapX), MathF.Round(mapY));
            var message =
                $"{name}\nLocation: ({gridX}, {gridY})\nGPS: ({mapX}, {mapY})";

            _trackedEntityLabel.Text = message;
            _trackedEntityPanel.Visible = true;

            return;
        }

        _trackedEntityLabel.Text = string.Empty;
        _trackedEntityPanel.Visible = false;
    }
}
