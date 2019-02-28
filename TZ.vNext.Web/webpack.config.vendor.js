const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
var CompressionPlugin = require('compression-webpack-plugin');

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    const extractCSS = new ExtractTextPlugin('vendor.css');

    return [{
        stats: { modules: false },
        resolve: { extensions: [ '.js' ] },
        entry: {
            vendor: [
                'bootstrap',
                'bootstrap/dist/css/bootstrap.css',
                'event-source-polyfill',
                'isomorphic-fetch',
                'jquery',
                'vue',
                'vue-router',
                'vue/dist/vue.js',
                // 'vue-property-decorator',
                // 'vue-class-component',
                //'iview',
                'element-ui',
                'element-ui/lib/theme-chalk/index.css',
                '@progress/kendo-ui',
                '@progress/kendo-ui/js/messages/kendo.messages.zh-CN.js',
                '@progress/kendo-grid-vue-wrapper',
                '@progress/kendo-datasource-vue-wrapper',
                '@progress/kendo-ui/css/web/kendo.common.min.css',
                '@progress/kendo-ui/css/web/kendo.common.core.min.css',
                '@progress/kendo-ui/css/web/kendo.silver.min.css',
                'font-awesome/css/font-awesome.min.css',
                //'fetch-download',
                'html-entities',
                'reflect-metadata',
                'fetch-intercept',
            ],
        },
        module: {
            rules: [
                { test: /\.css(\?|$)/, use: extractCSS.extract({ use: isDevBuild ? 'css-loader' : 'css-loader?minimize' }) },
                { test: /\.(png|jpg|jpeg|gif|svg|ttf|woff|woff2|eot)(\?|$)/, use: 'url-loader?limit=100000' }
            ]
        },
        output: { 
            path: path.join(__dirname, 'wwwroot', 'dist'),
            publicPath: '/dist/',
            filename: '[name].js',
            library: '[name]_[hash]'
        },
        plugins: [
            extractCSS,
            new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery' }), // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)
            new webpack.DefinePlugin({
                'process.env.NODE_ENV': isDevBuild ? '"development"' : '"production"'
            }),
            new webpack.DllPlugin({
                path: path.join(__dirname, 'wwwroot', 'dist', '[name]-manifest.json'),
                name: '[name]_[hash]'
            }),
            new CompressionPlugin({
                filename: "[path].gz[query]",
                algorithm: "gzip",
                test: /\.js$|\.css$|\.html$/,
                threshold: 10240,
                minRatio: 0.8
            }),
        ].concat(isDevBuild ? [] : [
            new webpack.optimize.UglifyJsPlugin()
        ])
    }];
};
