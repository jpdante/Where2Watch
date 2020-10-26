/*using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtcSharp.Core.Logging.Abstractions;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Where2Watch.Models;

namespace Where2Watch.Core.Search {
    public class SearchEngine {

        private const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

        private FSDirectory _indexDirectory;
        private Analyzer _analyzer;
        private IndexWriterConfig _indexWriterConfig;
        private IndexWriter _indexWriter;

        public Task Configure(string path) {
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
            _indexDirectory = FSDirectory.Open(path);
            _analyzer = new StandardAnalyzer(AppLuceneVersion);
            _indexWriterConfig = new IndexWriterConfig(AppLuceneVersion, _analyzer);
            _indexWriter = new IndexWriter(_indexDirectory, _indexWriterConfig);
            return Task.CompletedTask;
        }

        public void ClearIndex() {
            _indexWriter.DeleteAll();
            _indexWriter.Commit();
        }

        public void AddTitleIndex(Title title) {
            HtcPlugin.Logger.LogDebug($"Adding title: {title.OriginalName}");
            var doc = new Document {
                new Int64Field("id", title.Id, Field.Store.YES),
                new TextField("name", title.OriginalName.ToLower(), Field.Store.YES),
            };
            _indexWriter.AddDocument(doc);
            _indexWriter.Flush(false, false);
            _indexWriter.Commit();
        }

        public string[] SearchTitle(Query query, int n = 5) {
            using var reader = _indexWriter.GetReader(true);
            var searcher = new IndexSearcher(reader);
            var search = searcher.Search(query, n);
            return search.ScoreDocs.Select(result => searcher.Doc(result.Doc).Get("name")).ToArray();
        }

        public Query GetQuery(string field, string searchTerm) {
            var parser = new QueryParser(AppLuceneVersion, field, _analyzer);
            return parser.Parse(searchTerm);
        }
    }
}
*/