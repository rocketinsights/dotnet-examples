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

const getComposition = () => {

    let location = useLocation();
    let locale = parseLocale(location.pathname);

    let authToken = undefined;

    let context = {
        request: {
            uri: location.pathname.replace(`${locale.language}/${locale.country}/`, '')
        }
    }

    const requestConfig = {
        url: 'https://localhost:44345/experience/composition',
        headers: {
            authorization: authToken ? `Bearer ${authToken}` : "",
            acceptLanguage: `${locale.language}-${locale.country}`
        },
        context,
        maxRedirects: 0 // prevent axios from following redirects
    };

    axios(locationConfig)
        .then(({ data }) => {

            if (!data) {
                throw Error("ERROR");
            }

            return data;
        }
        );
}

export default getComposition;