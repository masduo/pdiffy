var path = require('path');
var paths = require('./paths');
var fileSync = require('fs');
var excludedFeatures = require('./excluded.features');

var apps = fileSync.readdirSync(paths.SRC.FEATURES);

excludedFeatures.forEach(function(feature) {
	apps.splice(feature, 1);
});

var entries = module.exports = [];

apps.forEach(function (app) {
	entries.push(path.join(paths.SRC.FEATURES, app, '*.scss'));
});