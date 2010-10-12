-- common functions not found in lua's main library
-- I'm a lua newb so this code style might be stupid
perseus = {}

perseus.string = {}
perseus.string.split = function (s, delim, count)	
	local t = {}
	local start = 1
		
	if count ~= nil and count <= 0 then return nil end
	if #s == 0 then return t end
	
	while true do
		if count ~= nil and #t == count - 1 then
			table.insert(t, string.sub(s, start))
			break;
		end
		
		-- find the next delimiter in the string
		local pos = perseus.string.findany(s, delim, start, true)
		if pos ~= nil then
			table.insert(t, string.sub(s, start, pos[1] - 1))
			start = pos[1] + #pos[2]
		else
			-- Add remaining
			table.insert(t, string.sub(s, start))
			break
		end
	end
	
	return t
end

-- similar to regular string.find, only it finds the first match of multiple patterns
perseus.string.findany = function (s, patterns, init, plain)
	if type(patterns) ~= "table" then
		patterns = {patterns}
	end
	
	if #patterns == 0 then return nil end
		
	local pos_current = string.find(s, patterns[1], init, plain)
	local value = patterns[1]
	
	for i = 2, #patterns do
		local pos = string.find(s, patterns[i], init, plain)
		if pos then
			if pos_current == nil or pos < pos_current then
				pos_current = pos
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
perseus.string.trim = function (s)  
	return (string.gsub(s, "^%s*(.-)%s*$", "%1"))
end

-- remove leading whitespace from string
perseus.string.ltrim = function (s)
	return (string.gsub(s, "^%s*", ''))
end

-- remove trailing whitespace from string
perseus.string.rtrim = function (s)
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

perseus.table = {}
-- map a function onto table values
perseus.table.map = function (t, func)
	local new_t = {}
	for i, v in pairs(t) do
		new_t[i] = func(v)
	end
	return new_t
end

-- removes empty items from a table
perseus.table.removeempty = function (t)
	local new_t = {}
	local index = 0
	local max = table.maxn(t)

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
	for _, v in pairs(t) do
		if v == element then
			return true
		end
	end
	return false
end

perseus.math = {}
perseus.math.randomize = function ()
	math.randomseed(os.time())
	math.random()
	math.random()
	math.random()
end

perseus.io = {}
perseus.io.dir = {}
perseus.io.dir.getfiles = function (path)	
	perseus.io.dir._getfiles(path)
	
	local files = perseus._tabledata
	perseus._tabledata = nil
	
	return files
end
perseus.io.dir.getdirs = function (path)
	perseus.io.dir._getdirs(path)
	
	local dirs = perseus._tabledata
	perseus._tabledata = nil
	
	return dirs
end