using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AoC.AoCUtils;

public static class ImageHandler
{
    public static void CreateImageFromMatrix<T>(Matrix<T> matrix, string fileName, T colorValue)
    {
        using(var image = new Image<Rgba32>(matrix.Columns, matrix.Rows)) 
        {
            foreach (var pos in matrix.GetAllPositions())
                image[pos.Column, pos.Row] = pos.Value != null && pos.Value.Equals(colorValue) ? Rgba32.ParseHex("FFFFFF"): Rgba32.ParseHex("000000");
            image.SaveAsJpeg(fileName);
        } 
    }

    public static Image<Rgba32> CreateImageFromStrings(string[] map, HashSet<(int Row, int Col)> values, (int x, int y, int dir)? block)
    {
        var image = new Image<Rgba32>(map[0].Length, map.Length);
        for (int row = 0; row < map.Length; row++)
        {
            for (int col = 0; col < map[row].Length; col++)
            {
                if(map[row][col] == '.')
                {
                    image[col, row] = Rgba32.ParseHex("000000");
                }
                else
                {
                    if(map[row][col] == '#')
                        image[col, row] = Rgba32.ParseHex("FFFFFF");
                    
                    //else if (values.TryGetValue((row, col), out var actualValue))
                //{
                //    image[col, row] = Rgba32.ParseHex("0000FF");
                //}
                
                }

                //if(block.HasValue && block.Value.x == col && block.Value.y == row)
                  //  image[col, row] = Rgba32.ParseHex("FF0000");
                    //else if (values.TryGetValue((row, col), out var actualValue))
                      //  image[col, row] = Rgba32.ParseHex("0000FF");
                    //else
                      //  image[col, row] = Rgba32.ParseHex("000000");
            }
        }
        //image.SaveAsGif($"c:\\temp\\test.gif");
        return image.Clone();
    }
}