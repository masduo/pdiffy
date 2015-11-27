var path = require('path');
var paths = require('./paths');
var fileSync = require('fs');

var apps = fileSync.readdirSync(paths.SRC.FEATURES);

var entries = module.exports = {};

apps.forEach(function (app) {

	var file = path.join(paths.SRC.FEATURES, app, (app + '.js'));
	if (fileSync.existsSync(file)) {
		entries[app] = file;
	}
});