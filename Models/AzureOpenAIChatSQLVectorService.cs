#nullable disable
using Azure.AI.OpenAI;
using AzureOpenAIChat.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureOpenAIChat.Data
{
    public class AzureOpenAIChatSQLVectorService
    {
        private readonly AzureOpenAIChatSQLVectorContext _context;
        public AzureOpenAIChatSQLVectorService(
            AzureOpenAIChatSQLVectorContext context)
        {
            _context = context;
        }

        public async Task<List<Article>>
            GetArticlesAsync()
        {
            // Get Articles including ArticleDetail           
            return await _context.Article
                .Include(a => a.ArticleDetail)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Article> GetArticleByIdAsync(int id)
        {
            // Get Article including ArticleDetail
            return await _context.Article
                .Include(a => a.ArticleDetail)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Add Article
        public async Task<Article> AddArticleAsync(Article article)
        {
            _context.Article.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        // Add ArticleDetail
        public async Task<ArticleDetail> AddArticleDetailAsync(
            ArticleDetail articleDetail, Embeddings embeddings)
        {
            _context.ArticleDetail.Add(articleDetail);

            await _context.SaveChangesAsync();

            // Get Embedding Vectors
            var EmbeddingVectors =
                embeddings.Data[0].Embedding
                .Select(d => (float)d).ToArray();

            // Instert all Embedding Vectors
            for (int i = 0; i < EmbeddingVectors.Length; i++)
            {
                var embeddingVector = new ArticleVectorData
                {
                    ArticleDetailId = articleDetail.Id,
                    VectorValueId = i,
                    VectorValue = EmbeddingVectors[i]
                };
                _context.ArticleVectorData.Add(embeddingVector);
            }

            await _context.SaveChangesAsync();

            return articleDetail;
        }

        // Delete Article
        public async Task<Article> DeleteArticleAsync(int id)
        {
            var article = await _context.Article.FindAsync(id);
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
            return article;
        }

        // GetSimilarContentArticles
        public List<SimilarContentArticlesResult>
            GetSimilarContentArticles(string vector)
        {
            return _context.SimilarContentArticles(vector).ToList();
        }
    }
}