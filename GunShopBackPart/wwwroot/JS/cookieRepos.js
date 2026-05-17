
export function parseJwt(token) {
    try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        return JSON.parse(atob(base64));
    } catch (e) {
        return null;
    }
}

export function getCookie(name, parseAsJwt = false) {
    const cookies = document.cookie.split("; ");

    for (const cookie of cookies) {
        const [key, value] = cookie.split("=");

        if (key === name) {

            const decodedValue = decodeURIComponent(value);

            if (parseAsJwt) {
                return parseJwt(decodedValue);
            }

            return decodedValue;
        }
    }

    return null;
}

    