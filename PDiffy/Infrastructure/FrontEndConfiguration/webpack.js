var path = require('path');
var entries = require('./scripts.entries');
var webpack = require('webpack');
var paths = require('./paths');

var webPack = module.exports = {
	src: entries['Differences', 'History'],
	entry: entries,
	output: {
		path: path.join(paths.SRC.SCRIPTS),
		filename: '[name].generated.js'
	},
	plugins: [new webpack.optimize.CommonsChunkPlugin('Shared.generated.js'),
	new webpack.optimize.DedupePlugin(),
	new webpack.optimize.UglifyJsPlugin(),
	new webpack.optimize.OccurenceOrderPlugin(),
	new webpack.optimize.AggressiveMergingPlugin()]
}