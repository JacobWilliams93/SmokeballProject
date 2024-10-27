import { useState } from "react";
import { SearchTypes } from "../enums/searchtype";
import SearchResults from "./SearchResults";

function SearchForm(){
    const [keywords, setKeywords] =  useState('');
    const [url, setUrl] = useState('');
    const [searchType, setSearchType] = useState(SearchTypes.Google);
    const [result, setResult] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    
    const search = async () => {
        try{
            setIsLoading(true);
            console.log(searchType);
            const response = await fetch(`http://localhost:5000/search?searchString=${keywords}&targetUrl=${url}&engineType=${searchType}`);
            const data = await response.json();
            setResult(data);
            setIsLoading(false);
        } catch (error){
            console.log(error);
        }
    }

    return (
    <>
        <div>
            <div className="field">
                <label>Keywords:</label>
                <input name="keywords" value={keywords} onChange={e => setKeywords(e.target.value)}/>
            </div>
            <div className="field"> 
                <label>Url: </label>
                <input name="url" value={url} onChange={e => setUrl(e.target.value)}/>
            </div>
            <div className="field">
                <label>Search Engine</label>
                <select value={searchType} onChange={e => {
                    const v: SearchTypes = SearchTypes[e.target.value as keyof typeof SearchTypes];
                    console.log(v);
                    setSearchType(v);
                }}>
                    {
                        Object.values(SearchTypes).filter(val => typeof val === "string").map( val => (
                            <option value={val} key={val}>
                                {val}
                            </option>
                        ))
                    }
                </select>
            </div>
        <button className="mt-[24px]" type="submit" onClick={search} disabled={isLoading}>{isLoading ? "Loading" : "Search"}</button>
      </div>

      {result && (
        <SearchResults
            keywords={keywords}
            results={result}
            searchEngine={searchType}
            url={url}
        />
      )}
    </>
    );
}

export default SearchForm;