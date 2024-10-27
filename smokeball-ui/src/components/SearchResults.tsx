import { SearchTypes } from "../enums/searchtype";

interface SearchResultsProps{
    keywords: string;
    url: string;
    searchEngine: SearchTypes;
    results: number[]
}

function SearchResults({results, keywords, url, searchEngine}: SearchResultsProps) {
    return (
        <>
            <h3>The URL {url} appears in search engine {searchEngine.toString()} with search term {keywords} at the following positions:</h3>
            <ul>
                {results.map(r => (
                    <li>{r}</li>
                ))}
            </ul>
        </>
    );
}

export default SearchResults;