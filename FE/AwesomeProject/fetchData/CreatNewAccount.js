import { urlHeader } from "../SetUp";

export default async function CreateNewAccount(username, password, email) {
    const url = `${urlHeader}/Client/SignUp/CreateClient`;
    const data = {
        id: 'abcdef',
        token: 'string',
        userNameAccount: username,
        passwordAccount: password,
        emailAccount: email,
        firstNameAccount: 'UserName',
        lastNameAccount: 'LastName',
        phoneNumberAccount: '0123456789',
        lastLoginDateAccount: '2024-04-11T14:27:36.793Z',
        createDateAccount: '2024-04-11T14:27:36.793Z'
    };

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'accept': '*/*'
            },
            body: JSON.stringify(data)
        });

        const responseData = await response.json();

    } catch (error) {
        console.error('Error creating account:', error);
        throw error;
    }
}