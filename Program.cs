﻿using Rss.Reader;

var processor = new Processor();
var urls = processor.ParseConfig("./urls.txt");
var feeds = processor.InstantiateFeeds(urls);

var display = new ConsoleDisplay(new HtmlParser());

await display.ShowFeedsAsync(feeds);