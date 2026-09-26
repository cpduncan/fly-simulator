import "./App.css";
import { Unity, useUnityContext } from "react-unity-webgl";

import { Provider } from "./components/ui/provider.tsx";
import { Button } from "@chakra-ui/react/button";

const unityBuildPath = `${import.meta.env.BASE_URL}assets/`;

function App() {
  const { unityProvider } = useUnityContext({
    loaderUrl: `${unityBuildPath}fly-sim-frontend-build-1.loader.js`,
    dataUrl: `${unityBuildPath}fly-sim-frontend-build-1.data`,
    frameworkUrl: `${unityBuildPath}fly-sim-frontend-build-1.framework.js`,
    codeUrl: `${unityBuildPath}fly-sim-frontend-build-1.wasm`,
  });

  return (
    <main className="unity-page">
      <div className="unity-container">
        <Unity className="unity-canvas" unityProvider={unityProvider} />
      </div>
      <div className="overlay-container">
        <Provider>
          <Button>Click</Button>
        </Provider>
      </div>
    </main>
  );
}

export default App;
