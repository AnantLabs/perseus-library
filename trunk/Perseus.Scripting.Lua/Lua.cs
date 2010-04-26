using System;
using System.IO;
using System.Reflection;

using LuaInterface;

namespace Perseus.Scripting.Lua {
    public class Lua {
        private LuaInterface.Lua _Lua;
        private LuaLibrary _Lib;

        public Lua() : this (true) { }
        public Lua(bool loadPerseusLibrary) {           
            this._Lua = new LuaInterface.Lua();
            
            if (loadPerseusLibrary) {
                this._Lib = new LuaLibrary(this._Lua);

                Assembly assembly = Assembly.GetExecutingAssembly();
                StreamReader sr = new StreamReader(
                    assembly.GetManifestResourceStream("Perseus.Scripting.Lua.perseus.lua")
                );

                if (sr == null) { throw new Exception("Could not load perseus.lua"); }
                this.DoString(sr.ReadToEnd() + " p = perseus");
                
                this.DoString("if perseus.clipboard == nil then perseus.clipboard = {} end");
                this.RegisterFunction("perseus.clipboard.settext", this._Lib, "ClipboardSetText");
                this.RegisterFunction("perseus.clipboard.gettext", this._Lib, "ClipboardGetText");
                this.RegisterFunction("perseus.clipboard.containstext", this._Lib, "ClipboardContainsText");
                
                this.DoString("if perseus.io == nil then perseus.io = {} end");
                
                this.DoString("if perseus.io.path == nil then perseus.io.path = {} end");
                this.DoString("perseus.io.path.separator = [[" + Path.DirectorySeparatorChar + "]]");
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

        public void LoadFile(string fileName) {
            // LUA doesn't play nice with BOM so use dostring instead of dofile
            string lua = File.ReadAllText(fileName);
            this._Lua.DoString(lua);            
        }

        public bool IsSet(string name) {
            return (bool)this._Lua.DoString("return (" + name + " ~= nil);")[0];
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

        public void DoString(string chunk) {
            this._Lua.DoString(chunk);
        }

        public LuaFunction RegisterFunction(string path, object target, string function) {
            Type t = target.GetType();
            return this._Lua.RegisterFunction(path, target, t.GetMethod(function));
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
        public void IOFileDelete(string path) {
            File.Delete(path);
        }
        public void IOFileCopy(string source, string dest) {
            File.Copy(source, dest);
        }
        public void IOFileMove(string source, string dest) {
            File.Move(source, dest);
        }
        public string IOFileReadAllText(string path) {
            return File.ReadAllText(path, System.Text.Encoding.UTF8);
        }
        public void IOFileWriteAllText(string path, string contents) {
            File.WriteAllText(path, contents, System.Text.Encoding.UTF8);
        }

        public string IOPathExtension(string path) {
            return Path.GetExtension(path);
        }
        public string IOPathFileName(string path) {            
            return Path.GetFileName(path);
        }
        public string IOPathFileNameWithoutExtension(string path) {
            return Path.GetFileNameWithoutExtension(path);
        }

        public bool IODirectoryCreate(string path) {
            DirectoryInfo di = Directory.CreateDirectory(path);
            return di.Exists;
        }
        public bool IODirectoryExists(string path) {
            return Directory.Exists(path);
        }
        public void IODirectoryDelete(string path) {
            Directory.Delete(path);
        }
        public void IODirectoryGetFiles(string path) {
            string[] files = Directory.GetFiles(path);

            this._Lua.DoString("p._tabledata = {}");
            for (int i = 0; i < files.Length; i++) {
                this._Lua.DoString("p._tabledata[" + (i + 1).ToString() + "] = [[" + files[i] + "]]");
            }
        }
        public void IODirectoryGetDirectories(string path) {
            string[] dirs = Directory.GetDirectories(path);

            this._Lua.DoString("p._tabledata = {}");
            for (int i = 0; i < dirs.Length; i++) {
                this._Lua.DoString("p._tabledata[" + (i + 1).ToString() + "] = [[" + dirs[i] + "]]");
            }
        }
    }   
}
