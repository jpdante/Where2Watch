import i18n from "i18next";
import { initReactI18next } from "react-i18next";

import LanguageDetector  from "i18next-browser-languagedetector";
import Backend  from "i18next-http-backend";

i18n
  .use(LanguageDetector)
  .use(Backend)
  .use(initReactI18next)
  .init({
    lng: "en",
    fallbackLng: "en",
    saveMissing: false,
    keySeparator: false,
    interpolation: {
      escapeValue: false,
    },
    /*backend: {
      loadPath: "/lang/{{lng}}.json",
    },

    debug: false,
    load: "languageOnly",
    ns: ["translations"],
    defaultNS: "translations",,
    react: {
      wait: true,
    },*/
  });

export default i18n;
