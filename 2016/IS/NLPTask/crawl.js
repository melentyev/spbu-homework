#!/usr/bin/env node
'use strict';
const cheerio = require('cheerio');
const request = require('request');
const iconv = require('iconv');

const argv = require('optimist').argv;

let getPage = (url, convert) => {
    return new Promise((resolve, reject) => { 
        let options = { uri: url, method: 'GET' };
        if (convert) { options.encoding = 'binary'; }
        request(options, function (error, response, body) {
            if (convert) {
                body = new Buffer(body, 'binary');
                let conv = new iconv.Iconv('windows-1251', 'utf8');
                body = conv.convert(body).toString();
            }
            resolve(body);
        })
    });
};

function downloadPages(current, last) {
    if (current > last) {
        process.exit(0);
    }
    getPage('http://ilibrary.ru/text/69/p.' + current + '/index.html', true)
        .then(contents => {
            let $ = cheerio.load(contents);

            let text = $('#text span.p').text();
            console.log(text);
            downloadPages(current + 1, last);
        })
        .catch((err) => { console.log(err); });
}

function dowloadWiki(url) {
    getPage(url)
        .then(contents => {
            let $ = cheerio.load(contents);
            
            $("script").remove();

            $("#content #bodyContent #mw-content-text .infobox.sisterproject.noprint").remove();
            $("#content #bodyContent #mw-content-text .references-small.columns").remove();
            $("#content #bodyContent #mw-content-text span.citation").remove();
            $("#content #bodyContent #mw-content-text span.mw-cite-backlink").remove();
            $("#content #bodyContent #mw-content-text table.standard").remove();
            
            $('#content #bodyContent #mw-content-text table.ruwikiWikimediaNavigation').remove();
            $('#content #bodyContent #mw-content-text table.navbox').remove();
            $('#content #bodyContent #mw-content-text span.mw-editsection').remove();
            
            $('#fa-message').remove();
            $('#toc').remove();
            
            let text = $('#content #bodyContent #mw-content-text').text();
            text = text
                .replace(/\[\d+\]/g, '')
                .replace(/\[en\]/g, '');
                
            console.log($("#firstHeading").text());
            console.log(text);
        })
        .catch((err) => { console.log(err); });
}

if (argv.d) {
    downloadPages(1, 40);
}
else if (argv.w) {
    dowloadWiki(argv.url);
}

