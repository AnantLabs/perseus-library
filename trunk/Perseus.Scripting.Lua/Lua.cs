using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

using LuaInterface;

using Perseus;

namespace Perseus.Scripting.Lua {
    public class Lua {
        private LuaInterface.Lua _Lua;
        private LuaLibrary _Lib;

        public Lua() : this (true) { }
        public Lua(bool loadPerseusLibrary) {           
            this._Lua = new LuaInterface.Lua();
           
            if (loadPerseusLibrary) {
                this._Lib = new LuaLibrary(this._Lua);

                string lua;

                // If a perseus.file exists in the assembly directory, use it instead of embeded one
                string luaFile = Assembly.GetExecutingAssembly().CodeBase;
                // Remove file:/// since File.Exists does not seem to support it
                if (luaFile.StartsWith("file:///", StringComparison.Ordinal)) {
                    luaFile = luaFile.Substring(8);
                }
                luaFile = Path.GetDirectoryName(luaFile) +
                    Path.DirectorySeparatorChar + 
                    "perseus.lua";
                
                if (File.Exists(luaFile)) {
                    lua = File.ReadAllText(luaFile);
                }
                else {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    StreamReader sr = new StreamReader(
                        assembly.GetManifestResourceStream("Perseus.Scripting.Lua.perseus.lua")
                    );

                    if (sr == null) { throw new Exception("Could not load perseus.lua"); }

                    lua = sr.ReadToEnd();
                }
                this.DoString(lua + " p = perseus");
                
                this.DoString("if perseus.clipboard == nil then perseus.clipboard = {} end");
                this.RegisterFunction("perseus.clipboard.settext", this._Lib, "ClipboardSetText");
                this.RegisterFunction("perseus.clipboard.gettext", this._Lib, "ClipboardGetText");
                this.RegisterFunction("perseus.clipboard.containstext", this._Lib, "ClipboardContainsText");
                
                this.DoString("if perseus.io == nil then perseus.io = {} end");
                
                this.DoString("if perseus.io.path == nil then perseus.io.path = {} end");
                this["perseus.io.path.separator"] = Path.DirectorySeparatorChar;
                this.RegisterFunction("perseus.io.path.extension", this._Lib, "IOPathExtension");
                this.RegisterFunction("perseus.io.path.filename", this._Lib, "IOPathFileName");
                this.RegisterFunction("perseus.io.path.basename", this._Lib, "IOPathFileNameWithoutExtension");
                
                this.DoString("if perseus.io.file == nil then perseus.io.file = {} end");
                this.RegisterFunction("perseus.io.file.exists", this._Lib, "IOFileExists");
                this.RegisterFunction("perseus.io.file.delete", this._Lib, "IOFileDelete");
                this.RegisterFunction("perseus.io.file.copy", this._Lib, "IOFileCopy");
                this.RegisterFunction("perseus.io.file.move", this._Lib, "IOFileMove");
                this.RegisterFunction("perseus.io.file.readtext", this._Lib, "IOFileReadAllText");
                this.RegisterFunction("perseus.io.file.writetext", this._Lib, "IOFileWriteAllText");

                this.DoString("if perseus.io.dir == nil then perseus.io.dir = {} end");
                this.RegisterFunction("perseus.io.dir.create", this._Lib, "IODirectoryCreate");
                this.RegisterFunction("perseus.io.dir.exists", this._Lib, "IODirectoryExists");
                this.RegisterFunction("perseus.io.dir.delete", this._Lib, "IODirectoryDelete");
                this.RegisterFunction("perseus.io.dir._getfiles", this._Lib, "IODirectoryGetFiles");
                this.RegisterFunction("perseus.io.dir._getdirs", this._Lib, "IODirectoryGetDirectories");                
            }   
        }
        ~Lua() {            
            this._Lua.Dispose();
        }

        public object this[string fullPath] {
            get {
                return this._Lua[fullPath];
            }
            set {
                this.EnsurePath(fullPath);
                if (value is IList) {
                    var list = value as IList;
                    this._Lua.NewTable(fullPath);
                    var table = this._Lua[fullPath] as LuaTable;

                    for (int i = 0; i < list.Count; ++i) {
                        table[i] = list[i];
                    }
                }
                else if (value is IDictionary) {
                    var dict = value as IDictionary;
                    this._Lua.NewTable(fullPath);
                    var table = this._Lua[fullPath] as LuaTable;

                    foreach(object o in dict.Keys) {
                        table[o] = dict[o];
                    }
                }
                else {
                    this._Lua[fullPath] = value;
                }
            }
        }

        public object LoadFile(string fileName) {
            // LUA doesn't play nice with BOM so use dostring instead of dofile
            string lua = File.ReadAllText(fileName);
            var result = this._Lua.DoString(lua);
            
            if (result != null) {
                return result[0];
            }

            return null;
        }

        public bool IsSet(string path) {
            return (bool)this._Lua.DoString("return (" + path + " ~= nil);")[0];
        }

        public object CallFunction(string name, params object[] args) {
            LuaFunction lf = this._Lua.GetFunction(name);
            return lf.Call(args)[0];
        }

        public LuaTable CallTableFunction(string name, params object[] args) {
            LuaFunction lf = this._Lua.GetFunction(name);
            object o = lf.Call(args)[0];

            if (o.GetType() != typeof(LuaTable)) {
                return null;
            }

            return (LuaTable)o;
        }
        
        public object DoString(string chunk) {
            var result = this._Lua.DoString(chunk);

            if (result != null) {
                return result[0];
            }

            return null;
        }

        public LuaFunction RegisterFunction(string path, object target, string function) {
            Type t = target.GetType();
            return this._Lua.RegisterFunction(path, target, t.GetMethod(function));
        }

        public static string EscapeString(string s) {
            var sb = new StringBuilder(s);
            sb.Replace("\a", "\\a");
            sb.Replace("\b", "\\b");
            sb.Replace("\f", "\\f");
            sb.Replace("\n", "\\n");
            sb.Replace("\r", "\\r");
            sb.Replace("\t", "\\t");
            sb.Replace("\v", "\\v");
            sb.Replace("\\", "\\\\");
            sb.Replace("\"", "\\\"");
            sb.Replace("'", "\\'");
            sb.Replace("[", "\\[");
            sb.Replace("]", "\\]");

            return sb.ToString();
        }

        private void EnsurePath(string path) {
            if (path.Trim().IsEmpty()) {
                return;
            }

            string[] parts = path.Split(".");
            string newPath = string.Empty;

            // We don't want the last one to be set to a table
            int len = parts.Length - 1;

            for (int i = 0; i < len; ++i) {
                if (newPath.IsEmpty()) {
                    newPath = parts[i];
                }
                else {
                    newPath += "." + parts[i];
                }

                if (!this.IsSet(newPath)) {
                    this._Lua.NewTable(newPath);
                }
            }
        }
    }
    internal class LuaLibrary {
        LuaInterface.Lua _Lua;
        public LuaLibrary(LuaInterface.Lua lua) {
            this._Lua = lua;
        }

        public void ClipboardSetText(string text) {
            System.Windows.Clipboard.SetText(text);
        }
        public string ClipboardGetText() {
            if (System.Windows.Clipboard.ContainsText()) {
                return System.Windows.Clipboard.GetText();
            }
            return string.Empty;
        }
        public bool ClipboardContainsText() {
            return System.Windows.Clipboard.ContainsText();
        }

        public bool IOFileExists(string path) {
            return File.Exists(path);
        }
        public bool IOFileDelete(string path) {
            if (File.Exists(path)) {
                try {
                    File.Delete(path);
                    return File.Exists(path);
                }
                catch {
                    return false;
                }
            }
            return false;
        }
        public bool IOFileCopy(string source, string dest) {
            try {
                File.Copy(source, dest);
            }
            catch {
                return false;
            }
            return true;
        }
        public bool IOFileMove(string source, string dest) {
            try {
                File.Move(source, dest);
            }
            catch {
                return false;
            }
            return true;
        }
        public string IOFileReadAllText(string path) {
            return File.ReadAllText(path, System.Text.Encoding.UTF8);
        }
        public bool IOFileWriteAllText(string path, string contents) {
            try {
                File.WriteAllText(path, contents, System.Text.Encoding.UTF8);                
            }
            catch {
                return false;
            }
            return true;
        }

        public string IOPathExtension(string path) {
            try {
                string extension = Path.GetExtension(path);
                if (extension == null) {
                    return string.Empty;
                }

                return extension;
            }
            catch {
                return string.Empty;
            }
        }
        public string IOPathFileName(string path) {                        
            try {
                string fileName = Path.GetFileName(path);
                if (fileName == null) {
                    return string.Empty;
                }

                return fileName;
            }
            catch {
                return string.Empty;
            }
        }
        public string IOPathFileNameWithoutExtension(string path) {            
            try {
                string baseName = Path.GetFileNameWithoutExtension(path);
                if (baseName == null) {
                    return string.Empty;
                }

                return baseName;
            }
            catch {
                return string.Empty;
            }
        }

        public bool IODirectoryCreate(string path) {
            try {
                DirectoryInfo di = Directory.CreateDirectory(path);
                return di.Exists;
            }
            catch {
                return false;
            }
        }
        public bool IODirectoryExists(string path) {
            return Directory.Exists(path);
        }
        public bool IODirectoryDelete(string path) {
            if (Directory.Exists(path)) {
                try {
                    Directory.Delete(path);
                    return Directory.Exists(path);
                }
                catch {
                    return false;
                }
            }
            return false;
        }
        public void IODirectoryGetFiles(string path) {
            string[] files = Directory.GetFiles(path);

            this._Lua.NewTable("perseus._tabledata");
            for (int i = 0; i < files.Length; i++) {
                ((LuaTable)this._Lua["perseus._tabledata"])[i + 1] = files[i];
            }
        }
        public void IODirectoryGetDirectories(string path) {
            string[] dirs = Directory.GetDirectories(path);
            
            this._Lua.NewTable("perseus._tabledata");
            for (int i = 0; i < dirs.Length; i++) {
                ((LuaTable)this._Lua["perseus._tabledata"])[i + 1] = dirs[i];
            }
        }
    }   
}
