var _deleteConfirmationString = "Are you sure you want to delete this item ?";
var _timerIntervalId;

function Hash()
{
	this.length = 0;
	this.items = new Array();
	for (var i = 0; i < arguments.length; i += 2) {
		if (typeof(arguments[i + 1]) != 'undefined') {
			this.items[arguments[i]] = arguments[i + 1];
			this.length++;
		}
	}
   
	this.removeItem = function(in_key)
	{
		var tmp_value;
		if (typeof(this.items[in_key]) != 'undefined') {
			this.length--;
			var tmp_value = this.items[in_key];
			delete this.items[in_key];
		}
	   
		return tmp_value;
	}

	this.getItem = function(in_key) {
		return this.items[in_key];
	}

	this.setItem = function(in_key, in_value)
	{
		if (typeof(in_value) != 'undefined') {
			if (typeof(this.items[in_key]) == 'undefined') {
				this.length++;
			}

			this.items[in_key] = in_value;
		}
	   
		return in_value;
	}

	this.hasItem = function(in_key)
	{
		return typeof(this.items[in_key]) != 'undefined';
	}
}

var _objectHash = new Hash();

function onactivate_timerStart()
{
	_timerIntervalId = setInterval(function(){ body_onload(); }, 100);
	document.detachEvent("onactivate", onactivate_timerStart);
}

function onactivate_timerStop()
{
	clearInterval(_timerIntervalId);
}

function getDocumentObject(id)
{		
	return(getDocumentObject2(id, false));
}

function getDocumentObject2(id, suppressErrors)
{	
	var returnObject = document.all(id);
	if (returnObject != null)
		return(returnObject);

	// Look in cache first	
	if (_objectHash.hasItem(id))
	{
		return(_objectHash.getItem(id));
	}
		
	// Return object that may be prefixed by user control name
	var idCount=0;
	for (var i=0; i<document.all.length; i++)
	{
		var fullId = document.all.item(i).id;
		if (fullId != "")
		{
			if (fullId.indexOf(id) > -1)
			{
				idCount++;
				returnObject = document.all.item(i);
				
				_objectHash.setItem(id, returnObject);
				break;
			}
		}	
	}
	
	if (!suppressErrors)
	{
		if (idCount == 0)
			alert("Cannot find " + id);
			
		if (idCount > 1)
			alert("Found " + idCount + " " + id);
	}
		
	return(returnObject);
}

function isDate(source, args)
{
	var date = new Date(args.Value);
	args.IsValid = !isNaN(date);
}

function isNumeric(source, args)
{
	args.IsValid = !isNaN(args.Value);
}
