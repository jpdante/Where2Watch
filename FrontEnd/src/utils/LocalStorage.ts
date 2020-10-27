export const TOKEN_KEY = "w2wt";
export const hasAuthenticationToken = () =>
  localStorage.getItem(TOKEN_KEY) !== null;
export const getAuthenticationToken = () => localStorage.getItem(TOKEN_KEY);
export const setAuthenticationToken = (token: string) => {
  localStorage.setItem(TOKEN_KEY, token);
};
export const deleteAuthenticationToken = () => {
  localStorage.removeItem(TOKEN_KEY);
};