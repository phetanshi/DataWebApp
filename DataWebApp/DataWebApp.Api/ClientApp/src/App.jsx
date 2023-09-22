import { Routes, Route } from "react-router-dom";
import { MsalProvider } from "@azure/msal-react";
import { MyIdeas } from "./pages/MyIdeas";
import { ReviewIdeas } from "./pages/ReviewIdeas";

import "./style/App.css";
import { PageLayout } from "./components/PageLayout";
import { CreateIdea } from "./pages/CreateIdea";
import { ViewIdea } from "./pages/ViewIdea";
import { ConfigProvider, theme } from "antd";
import { USER_SETTINGS, USER_THEMES } from "./config"
import { useState } from "react";

const Pages = () => {
    return (
        <Routes>
            <Route path="/reviewideas" element={<ReviewIdeas />} />
            <Route path="/createidea" element={<CreateIdea />} />
            <Route path="/viewidea" element={<ViewIdea />} />
            <Route path="/" element={<MyIdeas />} />
        </Routes>
    );
};

/**
 * msal-react is built on the React context API and all parts of your app that require authentication must be
 * wrapped in the MsalProvider component. You will first need to initialize an instance of PublicClientApplication
 * then pass this to MsalProvider as a prop. All components underneath MsalProvider will have access to the
 * PublicClientApplication instance via context as well as all hooks and components provided by msal-react. For more, visit:
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-react/docs/getting-started.md
 */
const App = ({ instance }) => {
    
    const [IsDarkTheme, setIsDarkTheme] = useState(USER_SETTINGS.theme === USER_THEMES.Dark);

    const getUserThemeSetting = () => {
        if (IsDarkTheme)
            return getDarkThemeSetting();
        return getLightThemeSetting();
    }

    const getDarkThemeSetting = () => {
        return {
            algorithm: theme.darkAlgorithm,
            token: {
                logoBackground: '#FFFFFF'
            }
        }
    }
    const getLightThemeSetting = () => {
        return {
            algorithm: theme.compactAlgorithm,
            token: {
                logoBackground: '#000000'
            }
        }
    }

    return (
        <MsalProvider instance={instance}>
            <ConfigProvider
                theme = {getUserThemeSetting()}
                // theme={{
                //     // algorithm: theme.compactAlgorithm
                //     algorithm: [theme.darkAlgorithm, theme.compactAlgorithm],
                //     token: {
                //         logoBackground: '#000000'
                //     }
                // }}
            >
                <PageLayout IsDarkTheme={IsDarkTheme} setIsDarkTheme={setIsDarkTheme}>
                    <Pages />
                </PageLayout>
            </ConfigProvider>
        </MsalProvider>
    );
};

export default App;
