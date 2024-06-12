const path = require('path');
const { VueLoaderPlugin } = require('vue-loader');
const outputPath = path.resolve(__dirname, '~/wwwroot/js/dist');

module.exports = {
    entry: './public/js/main.js',
    output: {
        filename: 'bundle.js',
        path: outputPath
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader'
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env']
                    }
                }
            },
            {
                test: /\.css$/,
                use: [
                    'style-loader',
                    'css-loader'
                ]
            }
        ]
    },
    plugins: [
        new VueLoaderPlugin()
    ],
    resolve: {
        alias: {
            'vue': 'vue/ dist / vue.esm - bundler.js'
        },
        extensions: ['.js', '.vue', '.json']
    },
    devServer: {
        static: outputPath,
        compress: true,
        port: 9000
    }
};
