// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using osuTK.Graphics;

namespace osu.Game.Overlays
{
    public abstract class OverlayHeader : Container
    {
        private readonly Box titleBackground;
        private readonly Box controlBackground;
        private readonly Container background;
        private readonly ScreenTitle title;

        protected float BackgroundHeight
        {
            set => background.Height = value;
        }

        protected OverlayColourScheme ColourScheme { get; }

        protected OverlayHeader(OverlayColourScheme colourScheme)
        {
            ColourScheme = colourScheme;

            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            Add(new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Children = new[]
                {
                    background = new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 80,
                        Masking = true,
                        Child = CreateBackground()
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Children = new Drawable[]
                        {
                            titleBackground = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                            },
                            title = CreateTitle().With(title =>
                            {
                                title.Margin = new MarginPadding
                                {
                                    Vertical = 10,
                                    Left = UserProfileOverlay.CONTENT_X_MARGIN
                                };
                            })
                        }
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Depth = -float.MaxValue,
                        Children = new Drawable[]
                        {
                            controlBackground = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = Color4.Gray,
                            },
                            CreateTabControl().With(control => control.Margin = new MarginPadding { Left = UserProfileOverlay.CONTENT_X_MARGIN })
                        }
                    },
                    CreateContent()
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            titleBackground.Colour = colours.ForOverlayElement(ColourScheme, 0.2f, 0.15f);
            title.AccentColour = colours.ForOverlayElement(ColourScheme, 1, 0.7f);
            controlBackground.Colour = colours.ForOverlayElement(ColourScheme, 0.2f, 0.2f);
        }

        protected abstract Drawable CreateBackground();

        [NotNull]
        protected virtual Drawable CreateContent() => new Container();

        protected abstract ScreenTitle CreateTitle();

        protected abstract TabControl<string> CreateTabControl();
    }
}
