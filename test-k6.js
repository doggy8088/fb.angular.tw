import http from 'k6/http';
import { check } from 'k6';
import { textSummary } from 'https://jslib.k6.io/k6-summary/0.0.2/index.js';

function check_http_302_redirection(url, status, expected_location) {
    const res = http.get(url, { redirects: 0 });
    check(res, { [`${url} status should be ${status}`]: (r) => r.status == status });
    check(res, { [`${url} Location header should be ${expected_location}`]: (r) => r.headers['Location'] == expected_location });
}

function check_http_200_ok(url) {
    const res = http.get(url, { redirects: 0 });
    check(res, { [`${url} status should be 200`]: (r) => r.status == 200 });
}

export default function () {
    check_http_200_ok('https://angular.tw/');
    check_http_200_ok('https://2017.angular.tw/');
    check_http_200_ok('https://2018.angular.tw/');
    check_http_200_ok('https://2019.angular.tw/');
    check_http_200_ok('https://forum.angular.tw/');
    check_http_200_ok('https://material.angular.tw/');
    check_http_200_ok('https://rxjs.angular.tw/');

    check_http_302_redirection('https://www.angular.tw/', 301, 'https://angular.tw/');
    check_http_302_redirection('https://cli.angular.tw/', 302, 'https://youtu.be/v4_YsDZbs3g');
    check_http_302_redirection('https://rx6.angular.tw/', 302, 'https://youtu.be/BA1vSZwzkK8');
    check_http_302_redirection('https://fb.angular.tw/', 302, 'https://www.facebook.com/groups/augularjs.tw');
    check_http_302_redirection('https://install.angular.tw/', 302, 'https://gist.github.com/doggy8088/15e434b43992cf25a78700438743774a');
    check_http_302_redirection('https://ts.angular.tw/', 302, 'https://willh.gitbook.io/typescript-tutorial');
    check_http_302_redirection('https://vscode.angular.tw/', 302, 'https://marketplace.visualstudio.com/items?itemName=doggy8088.angular-extension-pack');
    check_http_302_redirection('https://yt.angular.tw/', 302, 'https://www.youtube.com/c/AngularUserGroupTaiwan/videos');
}

export function handleSummary(data) {
    return {
        'stdout': textSummary(data, { indent: ' ', enableColors: true }),
    };
}

export const options = {
    vus: 1,
}