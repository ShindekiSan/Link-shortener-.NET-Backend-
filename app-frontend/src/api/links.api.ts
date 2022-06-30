import axios from 'axios';
import {
  LinkData, Link, LinkEdit, AddLink, LinkId, SearchedLinkData, SearchedLink,
} from '../types/link';

export const fetchLinks = async (userId: string):Promise<Link[]> => {
  const data = await axios({
    url: 'https://localhost:7279/api/link',
    method: 'GET',
    params: {
      userId,
    },
  });
  return data.data;
};

export const fetchLink = async (linkParams: LinkId):Promise<LinkData> => {
  const fetched = await axios({
    url: `https://localhost:7279/api/link/${linkParams.id}`,
    method: 'GET',
    params: {
      userId: linkParams.token,
    },
  });
  return { data: fetched.data[0] };
};

export const fetchLinkEdit = async (linkParams: LinkEdit):Promise<LinkData> => {
  const fetched = await axios({
    url: 'https://localhost:7279/api/link/edit',
    method: 'POST',
    data: {
      description: linkParams.description,
      tags: linkParams.tags,
      code: linkParams.code,
    },
  });
  return { data: fetched.data.link };
};

export const fetchNewLink = async (linkParams: AddLink):Promise<LinkData> => {
  const fetched = await axios({
    url: 'https://localhost:7279/api/link/generate',
    method: 'POST',
    data: {
      from: linkParams.from,
      tags: linkParams.tags,
      description: linkParams.description,
      ownerId: linkParams.token,
    },
  });
  return { data: fetched.data };
};

export const fetchSearchedLinks = async (tag: string):Promise<SearchedLink[]> => {
  const fetched = await axios({
    url: `https://localhost:7279/api/link/search/${tag}`,
    method: 'GET',
  });
  return fetched.data;
};

export const fetchSearchedLink = async (id: string | undefined):Promise<SearchedLinkData> => {
  const fetched = await axios({
    url: `https://localhost:7279/api/link/link-info/${id}`,
    method: 'GET',
  });
  return { data: fetched.data[0] };
};
