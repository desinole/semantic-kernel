### Models

Okay, here is a table that summarizes the main differences between the models:

| Model | Size | Domain | Availability |
|-------|------|--------|--------------|
| Ada | Smallest and cheapest | Natural language | OpenAI API or Azure OpenAI Service |
| Babbage | Larger and more expensive than Ada | Natural language | OpenAI API or Azure OpenAI Service |
| Curie | Larger and more expensive than Babbage | Natural language | OpenAI API or Azure OpenAI Service |
| Davinci | Largest and most expensive | Natural language or code | OpenAI API or Azure OpenAI Service |
| GPT | Generic name for the series of models | Natural language or code | Varies depending on the version |
| Codex | Similar to Davinci but specialized for code | Code | OpenAI API or Azure OpenAI Service |
| DALL·E | Similar to GPT-3 but specialized for images | Images | Beta access only |

I hope this helps you compare the models better.😊

The more parameters a model has, the more data it can process, learn from, and generate. However, having more parameters also means having more computational and memory resources, and more potential for overfitting or underfitting the data.


### Prompts

- Inputs or queries that a user or a program gives to an LLM AI, in order to elicit a specific response from the model. 
- Natural language sentences or questions, or code snippets or commands, or any combination
- Prompts can also be nested or chained, meaning that the output of one prompt can be used as the input of another prompt, creating more complex and dynamic interactions with the model.
- The art of creatively defining LLM AI prompts is an emerging field known as "prompt engineering". 
    - Key challenges include choosing the right words, phrases, symbols, and formats that can guide the model to generate high-quality and relevant texts. 
    - One can also experiment with different parameters and settings that can influence the behavior and performance of the model, such as temperature, top-k, top-p, frequency penalty, and presence penalty.

#### Prompt chaining

- This is a way of extending and enhancing the dialogue with the model, by using the generated texts as the basis for the next prompts
- help the model to learn from the feedback and the corrections that the user provides, and to adjust its behavior and output accordingly

#### Prompt Tuning

- Process of adapting and optimizing the prompts for specific tasks or domains, by using smaller and more specialized datasets.
- Improve the accuracy and the diversity of the generated texts, by reducing the noise and the bias that may exist in the general dataset.
- Prompt-tuning is an efficient, low-cost way of adapting an AI foundation model to new downstream tasks without retraining the model and updating its weights.

#### Prompt testing

- Process of measuring and comparing the quality and the usefulness of the prompts and the generated texts, by using various metrics and criteria.


### Token

You can think of tokens as pieces of words used for natural language processing. For English text, 1 token is approximately 4 characters or 0.75 words. As a point of reference, the collected works of Shakespeare are about 900,000 words or 1.2M tokens.

- Tokens: basic units of text/code for LLM AI models to process/generate language.
- Tokenization: splitting input/output texts into smaller units for LLM AI models.
- Vocabulary size: the number of tokens each model uses, which varies among different GPT models.
- Tokenization cost: affects the memory and computational resources that a model needs, which influences the cost and performance of running an OpenAI or Azure OpenAI model.

#### Tokenization

Tokenization is an essential step in the pipeline of LLMs, as it allows the models to process and analyze text in a structured format.
For example, the sentence “I love natural language processing” would be tokenized as [“I”, “love”, “natural”, “language”, “processing”]

ChatGPT uses Subword-based tokenization is usually performed using algorithms like Byte Pair Encoding (BPE). For example, the sentence “I love natural language processing” would be tokenized as [“I”, “love”, “nat”, “ural”, “language”, “process”, “ing”] using BPE.

#### Tokenizer

https://platform.openai.com/tokenizer

### Embeddings

Embeddings are a way of representing words or other types of data in a numerical form that can be easily used by machine learning models and algorithms. Embeddings capture the semantic meaning of the data, such that similar data points have similar embeddings.

Here are some examples of embeddings in natural language processing:

- Word embeddings: "dog" might have an embedding like [0.2, -0.5, 0.7, ...], while the word "cat" might have an embedding like [0.3, -0.4, 0.6, ...]. These embeddings are close in the vector space, indicating that they have similar meanings. Used for tasks like text classification, sentiment analysis, machine translation, etc.

- Sentence embeddings: "I like dogs" might have an embedding like [0.4, -0.2, 0.8, ...], while the sentence "I hate cats" might have an embedding like [-0.3, 0.1, -0.7, ...]. These embeddings are far apart in the vector space, indicating that they have different meanings and sentiments. Used for tasks like text summarization, question answering, natural language inference, etc.

- Graph embeddings: A node representing a person might have an embedding like [0.5, -0.3, 0.9, ...], while a node representing a movie might have an embedding like [0.2, 0.4, -0.6, ...]. These embeddings are different in the vector space, indicating that they belong to different types of entities. Used for tasks like link prediction, node classification, recommendation systems, etc.

### Vector Database

- allows for fast and accurate similarity search and retrieval of data based on their vector distance or similarity. 
- instead of using traditional methods of querying databases based on exact matches or predefined criteria, you can use a vector database to find the most similar or relevant data based on their semantic or contextual meaning.

To perform similarity search and retrieval in a vector database, you need to use a query vector that represents your desired information or criteria. The query vector can be either derived from the same type of data as the stored vectors (e.g., using an image as a query for an image database), or from different types of data (e.g., using text as a query for an image database). Then, you need to use a similarity measure that calculates how close or distant two vectors are in the vector space. The similarity measure can be based on various metrics, such as cosine similarity, euclidean distance, hamming distance, jaccard index.

The result of the similarity search and retrieval is usually a ranked list of vectors that have the highest similarity scores with the query vector. You can then access the corresponding raw data associated with each vector from the original source or index.

