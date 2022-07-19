import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import axios from "axios";

const parseLocale = (path) => {
    if (path.length < 6) {
        return false;
    }

    const parts = path.split("/").filter(part => part);
    const pathRegex = /^[A-Za-z]{2}$/;

    const testParts = (a, b) => pathRegex.test(a) && pathRegex.test(b);

    if (!testParts(parts[0], parts[1])) return false;

    return {
        country: parts[1].toLowerCase(),
        language: parts[0]
    };
};

const useComposition = () => {

    const location = useLocation();
    const [composition, setComposition] = useState({});

    useEffect(() => {
        let locale = parseLocale(location.pathname);

        let authToken = undefined;

        let path = location.pathname.replace(`${locale.language}/${locale.country}/`, '');

        let context = {
            uri: `https://www.example.com${path}`
        }

        console.log('created context', context)

        const requestConfig = {
            url: 'https://localhost:44345/experience/composition',
            method: 'POST',
            headers: {
                authorization: authToken ? `Bearer ${authToken}` : "",
                acceptLanguage: `${locale.language}-${locale.country}`
            },
            data: context,
            maxRedirects: 0 // prevent axios from following redirects
        };

        axios(requestConfig)
            .then(({ data }) => {

                if (!data) {
                    throw Error("ERROR");
                }

                console.log('Updated composition', data)

                setComposition(data);
            }
            );

    }, [location]);

    return { composition };
}

export default useComposition;