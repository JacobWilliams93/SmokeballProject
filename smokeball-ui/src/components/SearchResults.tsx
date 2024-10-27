import { SearchResultModel } from "../models/searchResultModel";

interface SearchResultsProps{
    results: SearchResultModel
}

function SearchResults({results}: SearchResultsProps) {
    return (
        <>
            <h3>The URL {results.url} appears in search engine {results.searchType.toString()} with search term {results.keywords} at the following positions:</h3>
            <ul>
                {results.searchPositions.map(r => (
                    <li>{r}</li>
                ))}
            </ul>
        </>
    );
}

export default SearchResults;