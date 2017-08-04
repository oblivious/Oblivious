var base = require("./webpack.config.base");
var path = require("path");
var webpack = require("webpack");
var merge = require("webpack-merge");

module.exports = merge(base, {
    devServer: {
        contentBase: './dist', // Relative directory for base of server
        hot: true, // Live-reload
        inline: true,
        port: 3000, // Port Number
        host: 'localhost', // Change to '0.0.0.0' for external facing server,
        historyApiFallback: {
            index: 'index.html'
        }
    },
    devtool: 'eval',
    entry: [
        'react-hot-loader/patch',
        'webpack-dev-server/client?http://localhost:3000',
        'webpack/hot/only-dev-server',
        './src/index.js'
    ],
    resolve: {
        alias: {
            config: path.join(__dirname, `config/local.js`)
        }
    },
    plugins: [
        new webpack.HotModuleReplacementPlugin(),
        new webpack.DefinePlugin({
            'process.env': {
                'NODE_ENV': JSON.stringify('development')
            }
        })
    ]
});