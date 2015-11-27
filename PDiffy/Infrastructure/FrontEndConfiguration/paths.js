var path = require('path');

var paths = module.exports = {};

paths.ROOT = path.join(__dirname, '..');
paths.SERVER_ROOT = path.join(paths.ROOT, '..');

paths.SRC = {
	FEATURES_SCSS: path.join(paths.SERVER_ROOT, 'Features', '**/*.scss'),
	FEATURES_JS: path.join(paths.SERVER_ROOT, 'Features', '*/*.js'),
	FEATURES: path.join(paths.SERVER_ROOT, 'Features', ''),
	SCRIPTS: path.join(paths.SERVER_ROOT, 'Scripts'),
	STYLES: path.join(paths.SERVER_ROOT, 'Styles')
};