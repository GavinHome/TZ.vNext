const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const bundleOutputDir = './wwwroot/dist';
const MinifyPlugin = require("babel-minify-webpack-plugin");
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CompressionPlugin = require('compression-webpack-plugin');
//const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;
//const SkeletonWebpackPlugin = require('vue-skeleton-webpack-plugin');
//const { SkeletonPlugin } = require('page-skeleton-webpack-plugin')

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);

    return [{
        stats: { modules: false },
        context: __dirname,
        resolve: {
            extensions: ['.js', '.ts', '.vue'],
            alias: {
                'vue': 'vue/dist/vue.js'
            }
        },
        entry: { 'main': './ClientApp/boot.ts' },
        module: {
            rules: [
                ////{ test: /\.vue\.html$/, include: /ClientApp/, loader: 'vue-loader', options: { loaders: { js: 'awesome-typescript-loader?silent=true' } } },
                {
                    test: /(js|vue|\.vue\.html)$/,
                    // test: /\.(js|vue|html|vue\.html)$/,
                    enforce: "pre",
                    include: /ClientApp/,
                    use: [
                        {
                            loader: 'eslint-loader',
                            options: {
                                formatter: require('eslint-friendly-formatter'),
                                configFile: "./.eslintrc.vue.js",
                                outputReport: {
                                    filePath: 'checkstyle.xml',
                                    formatter: require('eslint/lib/formatters/checkstyle')
                                }
                            }
                        },
                    ]
                },
                {
                    test: /\.vue\.html$/,
                    include: /ClientApp/,
                    use: [
                        {
                            loader: 'vue-loader',
                            options: {
                                loaders: {
                                    js: 'awesome-typescript-loader?silent=true'
                                }
                            }
                        }
                    ]
                },

                ////{ test: /\.ts$/, include: /ClientApp/, use: 'awesome-typescript-loader?silent=true' },
                // {
                //     test: /\.ts$/,
                //     enforce: 'pre',
                //     include: /ClientApp/,
                //     use: [
                //         // {
                //         //     loader: "tslint-loader",
                //         //     options: {
                //         //         formatter: 'stylish',
                //         //     }
                //         // },
                //         {
                //             loader: 'eslint-loader',
                //             options: {
                //                 formatter: require('eslint-friendly-formatter'),
                //                 configFile: "./.eslintrc.ts.js",
                //                 quiet: true,
                //                 failOnWarning: false,
                //                 failOnError: false,
                //                 outputReport: {
                //                     filePath: 'checkstyle.xml',
                //                     formatter: require('eslint/lib/formatters/checkstyle')
                //                 }
                //             }
                //         },
                //     ]
                // },
                {
                    test: /\.ts$/,
                    include: /ClientApp/,
                    use: [
                        {
                            loader: 'awesome-typescript-loader?silent=true'
                        }
                    ]
                },
                { test: /\.css$/, use: isDevBuild ? ['style-loader', 'css-loader'] : ExtractTextPlugin.extract({ use: 'css-loader?minimize' }) },
                { test: /\.(png|jpg|jpeg|gif|svg|ttf|woff|woff2|eot)$/, use: 'url-loader?limit=25000' }
            ]
        },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: '/dist/'
        },
        plugins: [
            new CheckerPlugin(),
            new webpack.DefinePlugin({
                'process.env': {
                    NODE_ENV: JSON.stringify(isDevBuild ? 'development' : 'production')
                }
            }),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            }),
            new HtmlWebpackPlugin({
                // Your HtmlWebpackPlugin config
                minify: {
                    removeComments: true
                }
            }),
            new webpack.LoaderOptionsPlugin({
                minimize: true
            }),
            new webpack.optimize.CommonsChunkPlugin({
                async: 'used-twice',
                minChunks: (module, count) => (
                    count >= 2
                ),
            }),
            new CompressionPlugin({
                filename: "[path].gz[query]",
                algorithm: "gzip",
                test: /\.js$|\.css$|\.html$/,
                threshold: 10240,
                minRatio: 0.8
            }),
            //new BundleAnalyzerPlugin(),
            // new webpack.ProvidePlugin({
            //     // "Promise": "es6-promise-promise"
            // }),
        ].concat(isDevBuild ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
            })
        ] : [
                // Plugins that apply in production builds only
                //new webpack.optimize.UglifyJsPlugin(),
                new MinifyPlugin({}, { sourceMap: false, comments: false }),
                new ExtractTextPlugin('site.css')
            ])
    }];
};
