# StringMetrics Project

Welcome to the StringMetrics project! This project is a custom implementation of seven different string metric algorithms designed to evaluate the similarity and distance between two strings. My goal is to provide an easy-to-use and extensible framework for string comparison using well-known metrics.

## Features

### Custom Implementation:

Each string metric is a custom implementation conforming to the _IMetric_ interface.

**Metrics Included:**

- **_Hamming:_** Assigns a score based on the number of positions at which the corresponding symbols are different.
- **_Dice:_** Measures the similarity between two sets.
- **_Jaro:_** Evaluates the similarity and assigns a score based on the matching characters and transpositions.
- **_Jaro-Winkler:_** An extension of Jaro that gives more favorable ratings to strings that match from the beginning.
- **_Soundex:_** Encodes strings into a sound representation based on how they are pronounced in English.
- **_Levenshtein:_** Represents the number of single-character edits required to change one word into the other.
- **_Damerau-Levenshtein:_** Extends Levenshtein by including transpositions among its allowable operations.

**IAlgorithm Interface:**
Each metric implements the IMetric interface, providing a standardized way to access and use the metrics:

- **_double Compare(string left, string right)_**: Compares two strings and returns the similarity score.
- **_string Left { get; }_**: Gets the left comparison string.
- **_string Right { get; }_**: Gets the right comparison string.
- **_double Score { get; }_**: Gets the similarity score between Left and Right.
- **_double Distance { get; }_**: Gets the metric specific distance score between Left and Right.
- **_IDiagnostics Diagnostics { get; }_**: Provides diagnostic information about the comparison, e.g. total milliseconds elapsed to compare two strings.

### Usage

To use this library, include it in your project and instantiate the desired string metric class using _Factory_ static class provided in this project. Call the Compare method with two strings to evaluate their similarity:

**_C# / CSharp_**

```
StringMetrics.IMetric sim = ikatic.StringMetrics.Factory.Create(ikatic.StringMetrics.Metrics.Hamming) as StringMetrics.IMetric;
WriteLine(sim.Compare("House", "Hause"));
WriteLine(sim.Compare("The cat napped on the sunny mat", "The dog slept on the cozy bed"));
```

**_Output_**

`0.8` <br>
`0.1935483870967742`

## String Metrics and Their Uses and Applications

String metrics are mathematical algorithms designed to measure the similarity or distance between two text strings. They play a crucial role in various applications, from data cleansing to natural language processing. By comparing strings for similarity, these metrics help identify and resolve inconsistencies in data, detect duplicates, and improve data quality and integrity.

### Data Cleansing

In data cleansing, string metrics help identify and correct inconsistencies in datasets. For example, they can be used to standardize names, addresses, and other textual information, making data more reliable and usable.

### Duplication Identification

String metrics are invaluable for identifying duplicate records in databases. By measuring the similarity between strings, algorithms can detect potential duplicates that might not be exact matches due to typos, variations in spelling, or different formatting.

### Record Linkage

In databases where records span multiple datasets, string metrics can help link related records. This is crucial in fields like healthcare or customer relationship management, where multiple records for the same entity may exist across different systems.

### Search and Information Retrieval

String metrics can enhance search functionality by allowing fuzzy matching. This means users can obtain relevant results even if they misspell search terms or enter only partial queries.

### Natural Language Processing (NLP)

In NLP, string metrics aid in tasks such as text summarization, sentiment analysis, and language translation by helping to measure and analyze text similarity and variance.

### Fraud Detection

String metrics can be used to compare textual data in financial or security contexts, helping to identify fraudulent activities where slight variations in textual information might indicate deceptive behavior.

### Other Uses

- **_Spell Checking and Correction:_** By comparing input words to a dictionary, string metrics can suggest corrections for misspelled words.

- **_Plagiarism Detection:_** In academic and literary fields, string metrics can help identify instances of plagiarism by measuring how closely texts match.

- **_Genetic Sequencing:_** In bioinformatics, string metrics compare DNA sequences to identify genetic similarities and differences.

- **_Customer Feedback Analysis:_** Businesses can use string metrics to analyze customer feedback, identifying common themes or issues even when expressed differently by various customers.

## Contribution

I welcome contributions to the StringMetrics project! Whether it's improving the algorithms, fixing bugs, or extending the documentation, your help is appreciated.

## License

This project is open-source and available under the MIT License.
Feel free to fork the project, submit pull requests, or report issues. Your feedback and contributions are valued as we strive to improve this implementation for everyone.
