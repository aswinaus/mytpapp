﻿#nullable disable
using AzureOpenAIChat;

namespace AzureOpenAIChat.Models.DTO
{
    public class EmbeddingDTO
    {
        public string Text { get; set; }
        public float[] Values { get; set; }
    }
}
