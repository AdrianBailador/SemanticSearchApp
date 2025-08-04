---

# SemanticSearchApp

A minimal **ASP.NET Core** API demonstrating **Semantic Search** using **OpenAI embeddings**, **SQLite**, and **Dapper**.

This project shows how to build a simple yet powerful backend that understands the meaning of text queries to find relevant documents, not just exact keyword matches.

---

## 🚀 Features

* Minimal API with .NET
* Store documents with embeddings in SQLite
* Generate text embeddings via OpenAI API
* Search documents by semantic similarity (cosine similarity)
* Basic error handling for API key and request issues
* Easy to extend and adapt for your own projects

---

## ⚙️ Setup

1. Clone the repository

```bash
git clone https://github.com/AdrianBailador/SemanticSearchApp.git
cd SemanticSearchApp
```

2. Set your OpenAI API key as an environment variable:

```bash
export OPENAI_API_KEY=sk-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
```

3. Run the app:

```bash
dotnet run
```

---

## 📦 API Endpoints

| Method | Endpoint        | Description                                        |
| ------ | --------------- | -------------------------------------------------- |
| POST   | `/documents`    | Add a document and generate its embedding          |
| GET    | `/search?q=...` | Search for documents semantically similar to query |

---

## 🔍 Example Usage

### Add Document

```bash
curl -X POST http://localhost:5000/documents \
  -H "Content-Type: application/json" \
  -d '{"title": "ASP.NET Guide", "content": "ASP.NET Core is a powerful web framework..."}'
```

### Search Documents

```bash
curl "http://localhost:5000/search?q=powerful web framework"
```

Example JSON response:

```json
[
  {
    "title": "ASP.NET Guide",
    "content": "ASP.NET Core is a powerful web framework...",
    "score": 0.92
  }
]
```

---


