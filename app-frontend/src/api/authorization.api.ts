import axios from 'axios';
import { SignupData, UserData, LoginData } from '../types/user';

export const fetchAuthorization = async (user: LoginData):Promise<UserData> => {
  const fetched = await axios({
    method: 'POST',
    url: 'https://localhost:7279/api/auth/login',
    data: {
      email: user.email,
      password: user.password,
    },
  });
  return { data: fetched.data };
};

export const fetchRegistration = async (user: SignupData):Promise<UserData> => {
  const fetched = await axios({
    method: 'POST',
    url: 'https://localhost:7279/api/auth/register',
    data: {
      email: user.email,
      password: user.password,
      username: user.username,
    },
  });
  return { data: fetched.data };
};

export const fetchCurrentUser = async (id: string):Promise<UserData> => {
  const fetched = await axios({
    method: 'GET',
    url: `https://localhost:7279/api/auth/get-user/${id}`,
  });
  return { data: fetched.data.user };
};
