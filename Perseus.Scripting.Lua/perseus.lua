-- common functions not found in lua's main library
-- I'm a lua newb so this code style might be stupid
perseus = {}
perseus.version = "1.1"

perseus.string = {}
perseus.string.compareversion = function(v1, v2)
	v1 = perseus.string.split(v1, ".")
	v1 = perseus.table.map(v1, tonumber)
	v2 = perseus.string.split(v2, ".")
	v2 = perseus.table.map(v2, tonumber)

	local i, v, ci
	for i, v in pairs(v1) do                
		if v2[i] == nil then
			if v ~= 0 then return 1 end
		elseif v2[i] > v then
			return -1	
		elseif v2[i] < v then
			return 1
		end
        
        ci = i
	end
    
	ci = ci + 1
	while v2[ci] ~= nil do
		if v2[ci] ~= 0 then return -1 end

		ci = ci + 1
	end

	return 0 
end

perseus.string.split = function(s, delim, count)	
	local t = {}
	local start = 1
		
	if count ~= nil and count <= 0 then return nil end
	if #s == 0 then return t end
	
	while true do
		if count ~= nil and #t == count - 1 then
			table.insert(t, string.sub(s, start))
			break
		end
		
		-- find the next delimiter in the string
		local pos = perseus.string.findany(s, delim, start, true)
		if pos ~= nil then
			table.insert(t, string.sub(s, start, pos[1] - 1))
			start = pos[1] + #pos[2]
		else
			-- add remaining
			table.insert(t, string.sub(s, start))
			break
		end
	end
	
	return t
end

-- similar to regular string.find, only it finds the first match of multiple patterns
perseus.string.findany = function(s, patterns, init, plain)
	if type(patterns) ~= "table" then
		patterns = {patterns}
	end
	
	if #patterns == 0 then return nil end
		
	local pos_current = string.find(s, patterns[1], init, plain)
	local value = patterns[1]
	
	local i
	for i = 2, #patterns do
		local pos = string.find(s, patterns[i], init, plain)
		if pos ~= nil then
			if pos_current == nil or pos < pos_current then
				pos_current = pos
				value = patterns[i]
			-- in cases where 1 delimiter starts with another delimiter, choose the longer one	
			elseif pos == pos_current and #patterns[i] > #value then
				value = patterns[i]
			end
		end
	end
	
	if pos_current == nil then return nil end
	
	return {pos_current, value}
end

-- remove trailing and leading whitespace from string
perseus.string.trim = function(s)  
	return (string.gsub(s, "^%s*(.-)%s*$", "%1"))
end

-- remove leading whitespace from string
perseus.string.ltrim = function(s)
	return (string.gsub(s, "^%s*", ''))
end

-- remove trailing whitespace from string
perseus.string.rtrim = function(s)
	local n = #s
	while n > 0 and string.find(s, "^%s", n) do n = n - 1 end
	return string.sub(s, 1, n)
end

-- returns true if the string starts with the specified value; otherwise false
perseus.string.startswith = function(s, with)
	return (string.sub(s, 1, #with) == with)
end

-- returns true if the string ends with the specified value; otherwise false
perseus.string.endswith = function(s, with)
	return (string.sub(s, -#with) == with)
end

perseus.string.lpad = function(s, len, char)
    if char == nil then char = " " end
    return s .. string.rep(char, len - #s)
end

perseus.string.rpad = function(s, len, char)
    if char == nil then char = " " end
    return string.rep(char, len - #s) .. s
end

perseus.table = {}
-- map a function onto table values
perseus.table.map = function(t, func)
	local new_t = {}
	
	local i, v
	for i, v in pairs(t) do
		new_t[i] = func(v)
	end
	return new_t
end

-- removes empty items from a table
perseus.table.removeempty = function(t)
	local new_t = {}
	local index = 0
	local max = table.maxn(t)
	
	local i, v
	for i, v in pairs(t) do
		if t[i] ~= "" then
			-- if its an indexed value, rebuild the index
			if type(i) == "number" and i >= 1 and i <= max then
				index = index + 1
				new_t[index] = v
			else
				new_t[i] = v
			end
		end
	end

	return new_t
end

perseus.table.contains = function(t, element)
	local v
	for _, v in pairs(t) do
		if v == element then
			return true
		end
	end
	return false
end

perseus.table.count = function(t)
	local n = 0
	local v
	for _, v in pairs(t) do
		n = n + 1
	end
	return n
end

perseus.table.copy = function(t, deep)
	-- keep track of self references
    local lookup = {}
    
    local function copy(t, first)
        if not first and not deep or type(t) ~= "table" then
            return t
        elseif lookup[t] then
            return lookup[t]
        end
        
        local new_t = {}
        lookup[t] = new_t
        
        local i, v
        for i, v in pairs(t) do
            new_t[copy(i)] = copy(v)
        end

        return setmetatable(new_t, getmetatable(t))
    end
    
    return copy(t, true)
end

--[[
    Taken from http://lua-users.org/wiki/TableSerialization
]]
perseus.table.show = function(t, name)
   local cart = ""    -- a container
   local autoref = "" -- for self references

   name = name or "__unnamed__"

   local function isemptytable(t) return next(t) == nil end

   local function basicSerialize(o)
      local so = tostring(o)
      if type(o) == "function" then
         local info = debug.getinfo(o, "S")
         -- info.name is nil because o is not a calling level
         if info.what == "C" then
            return string.format("%q", so .. ", C function")
         else 
            -- the information is defined through lines
            return string.format("%q", so .. ", defined in (" ..
                info.linedefined .. "-" .. info.lastlinedefined ..
                ")" .. info.source)
         end
      elseif type(o) == "number" or type(o) == "boolean" then
         return so
      else
         return string.format("%q", so)
      end
   end

   local function addtocart(value, name, indent, saved, field)
      indent = indent or ""
      saved = saved or {}
      field = field or name

      cart = cart .. indent .. field

      if type(value) ~= "table" then
         cart = cart .. " = " .. basicSerialize(value) .. ";\n"
      else
         if saved[value] then
            cart = cart .. " = {}; -- " .. saved[value] .. " (self reference)\n"
            autoref = autoref .. name .. " = " .. saved[value] .. ";\n"
         else
            saved[value] = name
            --if tablecount(value) == 0 then
            if isemptytable(value) then
               cart = cart .. " = {};\n"
            else
               cart = cart .. " = {\n"
               for k, v in pairs(value) do
                  k = basicSerialize(k)
                  local fname = string.format("%s[%s]", name, k)
                  field = string.format("[%s]", k)
                  -- three spaces between levels
                  addtocart(v, fname, indent .. "   ", saved, field)
               end
               cart = cart .. indent .. "};\n"
            end
         end
      end
   end
   
   if type(t) ~= "table" then
      return name .. " = " .. basicSerialize(t)
   end
   
   addtocart(t, name, indent)
   
   return cart .. autoref
end

perseus.math = {}
perseus.math.randomize = function()
	math.randomseed(os.time())
	math.random()
	math.random()
	math.random()
end

perseus.io = {}
perseus.io.dir = {}
perseus.io.dir.getfiles = function(path)	
	perseus.io.dir._getfiles(path)
	
	local files = perseus.table.copy(perseus._tabledata, true)
	perseus._tabledata = nil
	
	return files
end
perseus.io.dir.getdirs = function(path)
	perseus.io.dir._getdirs(path)
	
	local dirs = perseus.table.copy(perseus._tabledata, true)
	perseus._tabledata = nil
	
	return dirs
end