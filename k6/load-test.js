import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  insecureSkipTLSVerify: true,
    noConnectionReuse: false,    
    stages: [
        { duration: '2m', target: 100 }, // ramp-up 
        { duration: '2m', target: 100 }, // stay high
        { duration: '30s', target: 0 }, // ramp-down
    ]
};

const API_BASE_URL = 'https://jobs.webapi/';

export function setup() {
    const model = JSON.stringify({ firstname: randomString(10), lastname: randomString(30) });
    const headers = { 'Content-Type': 'application/json' };
    const result = http.post(API_BASE_URL + "users", model, { headers });
    check(result, { 'created user': (r) => r.status === 200 });
    return result.json('userId');
}

export default (data) => {
    const model = JSON.stringify({ userId: data });
    const headers = { 'Content-Type': 'application/json' };
    const result = http.post(API_BASE_URL + "jobs/create-and-execute", model, { headers });
    check(result, { 'created job': (r) => r.status === 200 });
    sleep(1);
}

function randomString(length, charset = '') {
    if (!charset) charset = 'abcdefghijklmnopqrstuvwxyz';
    let res = '';
    while (length--) res += charset[(Math.random() * charset.length) | 0];
    return res;
}