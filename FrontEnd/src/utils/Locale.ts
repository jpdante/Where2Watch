export function getCountryByLanguage() {
  let language = navigator.language;
  let data = language.split("-", 2);
  if (data.length === 2) {
    return data[1].substring(0, 2);
  }
  return "US";
}
