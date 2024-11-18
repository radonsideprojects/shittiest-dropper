using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using dnlib.DotNet.Emit;
using dnlib.DotNet;

namespace Builder
{
    class DBuilder
    {
        // https://raw.githubusercontent.com/mariglenpupa/BinaryMod/refs/heads/main/Program.cs
        public static void Build(string path, byte[] file, string[,] replacements)
        {
            ModuleDefMD asmDef = ModuleDefMD.Load(file);
            Modify(asmDef, path, replacements);
            asmDef.Write(path);
            asmDef.Dispose();
            MessageBox.Show("Dropper build!", "Done!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private static void Modify(ModuleDefMD asmDef, string AsmName, string[,] rep)
        {
            try
            {
                foreach (TypeDef type in asmDef.Types)
                {
                    asmDef.Assembly.Name = Path.GetFileNameWithoutExtension(AsmName);
                    asmDef.Name = Path.GetFileName(AsmName);
                    foreach (MethodDef method in type.Methods)
                    {
                        if (method.Body == null) continue;
                        for (int i = 0; i < method.Body.Instructions.Count(); i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                            {
                                for (int k = rep.Length / 2; k > 0; k--)
                                {
                                    if (method.Body.Instructions[i].Operand.ToString() == rep[k - 1, 0])
                                        method.Body.Instructions[i].Operand = rep[k - 1, 1];
                                }
                            }
                        }
                    }
                }
            }
            catch { throw; }
        }
    }
}