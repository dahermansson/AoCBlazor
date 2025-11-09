using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AoC.AoCUtils;

public class GifGenerator()
{
    private readonly List<Image<Rgba32>> Frames = [];

    public void AddFrame(Image<Rgba32> image)
    {
        Frames.Add(image);
    }

    public void CreateGif(string filename = "output.gif")
    {
        using Image<Rgba32> gif = new(Frames.First().Width, Frames.First().Height);
        Frames.ForEach(frame => {
            gif.Frames.AddFrame(frame.Frames[0]);
            });
        gif.SaveAsGif(filename);
    }
}
