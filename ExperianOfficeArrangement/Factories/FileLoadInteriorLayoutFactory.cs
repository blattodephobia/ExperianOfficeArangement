using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExperianOfficeArrangement.Models;
using Microsoft.Win32;
using System.IO;

namespace ExperianOfficeArrangement.Factories
{
    public class FileLoadInteriorLayoutFactory : LayoutFactoryBase
    {
        public override InteriorField[,] GetLayout()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "txt";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                this.LayoutIdentifier = openFileDialog.FileName;
                using (StreamReader readLines = new StreamReader(openFileDialog.OpenFile()))
                {
                    List<List<InteriorField>> layout = readLines
                        .ReadToEnd()
                        .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s
                            .Select(c => this.CreateField(c)).ToList()).ToList();

                    int rowsCount = layout.Max(l => l.Count);
                    InteriorField[,] result = new InteriorField[rowsCount, layout.Count];
                    for (int y = 0; y < layout.Count; y++)
                    {
                        for (int x = 0; x < layout[y].Count; x++)
                        {
                            result[x, y] = layout[y][x];
                        }
                    }
                    return result;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
