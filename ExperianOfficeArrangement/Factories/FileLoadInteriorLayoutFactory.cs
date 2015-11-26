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
        private static readonly string DataSubdirName = "Data";

        public override InteriorField[,] GetLayout()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "txt";
            DirectoryInfo currentDir = new DirectoryInfo(Environment.CurrentDirectory);
            openFileDialog.InitialDirectory = (currentDir.EnumerateDirectories().FirstOrDefault(d => d.Name == DataSubdirName) ?? currentDir).FullName;
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

                    int columnsCount = layout.Max(l => l.Count);
                    InteriorField[,] result = new InteriorField[layout.Count, columnsCount];
                    for (int row = 0; row < layout.Count; row++)
                    {
                        for (int col = 0; col < layout[row].Count; col++)
                        {
                            result[row, col] = layout[row][col];
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
