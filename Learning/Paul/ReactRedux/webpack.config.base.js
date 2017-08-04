
const webpack = require('webpack');
const path = require('path');
const nodeModulesPath = path.resolve(__dirname, 'node_modules');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const ImageminPlugin = require('imagemin-webpack-plugin').default

const config = {
    entry: [path.join(__dirname, '/src/index.js')],
    // output config
    output: {
        path: path.join(__dirname, 'dist'),
        filename: '[name].js',
        chunkFilename: '[name].chunk.js',
        publicPath: '/'
    },
    plugins: [
        new CleanWebpackPlugin(['dist'], {
            verbose: true,
            dry: false
        }),

        // Allows error warnings but does not stop compiling.
        new webpack.NoEmitOnErrorsPlugin(),        
        new HtmlWebpackPlugin({
            template: './src/index.html',
            chunks: ['main'],
            filename: 'index.html',
        }),
        new webpack.optimize.CommonsChunkPlugin({
            name: 'main',
            chunks: ['commons'],
        }),
        new CopyWebpackPlugin([
            { from: path.join(__dirname, '/src/static') }
        ]),    
        new ImageminPlugin({ test: /\.(jpe?g|png|gif|svg)$/i }),
    ],
    resolve: {
        extensions: ['.js', '.jsx'],
        alias: {
            sass: path.join(__dirname, `src/sass`),
            components: path.join(__dirname, `src/components`),
            pages: path.join(__dirname, `src/pages`)
        }
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/, // All .js files,
                include: [
                    path.resolve(__dirname, "src")
                ],
                exclude: [
                    path.resolve(__dirname, "node_modules")
                ],
                use: [
                    {
                        loader: 'babel-loader',
                        query: {
                            plugins: ['transform-decorators-legacy',
                                'transform-runtime',
                                'transform-object-rest-spread',
                                'transform-react-constant-elements',
                                'transform-class-properties'],
                            presets: [['es2015', { modules: false }], 'latest', 'react']
                        }
                    }
                ]
            },
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader']
            },
            {
                test: /\.scss$/,
                use: ['style-loader', 'css-loader', 'sass-loader']
            }
        ],
    }
};

module.exports = config;
