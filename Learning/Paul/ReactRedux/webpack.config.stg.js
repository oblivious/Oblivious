var base = require("./webpack.config.base");
var path = require("path");
var webpack = require("webpack");
var merge = require("webpack-merge");

module.exports = merge(base, {
    devtool: 'source-map',
    resolve: {
        alias: {
            config: path.join(__dirname, `config/staging.js`),
        }
    },
    plugins: [
        new webpack.DefinePlugin({
            'process.env': {
                'NODE_ENV': JSON.stringify('production')
            }
        })
    ]
});