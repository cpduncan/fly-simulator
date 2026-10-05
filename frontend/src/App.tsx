import "./App.css";
import { Unity, useUnityContext } from "react-unity-webgl";

import { Provider } from "./components/ui/provider.tsx";

import {
  Stat_CumulativeTotalFood,
  Stat_GenTotalFood,
  Stat_GenAvgFood,
} from "./components/ui/stats.tsx";
import "./components/ui/stats.css";

import { SensoryDocumentation } from "./components/ui/accordion.tsx";

const unityBuildPath = `${import.meta.env.BASE_URL}assets/`;

function App() {
  const { unityProvider } = useUnityContext({
    loaderUrl: `${unityBuildPath}fly-sim-frontend-build-3.loader.js`,
    dataUrl: `${unityBuildPath}fly-sim-frontend-build-3.data`,
    frameworkUrl: `${unityBuildPath}fly-sim-frontend-build-3.framework.js`,
    codeUrl: `${unityBuildPath}fly-sim-frontend-build-3.wasm`,
  });

  return (
    <main className="unity-page">
      <div className="unity-container">
        <Unity className="unity-canvas" unityProvider={unityProvider} />
      </div>
      <div className="overlay-container">
        <Provider>
          <div className="overlay-grid">
            <div className="overlay-left">
              <h2>Generation Stats</h2>
              <div className="left-stat-grid">
                <Stat_CumulativeTotalFood />
                <Stat_GenTotalFood />
                <Stat_GenAvgFood />
              </div>
            </div>
            <div className="overlay-center"></div>
            <div className="overlay-right">
              <h2>Sensory Documentation</h2>
              <SensoryDocumentation />
            </div>
          </div>
        </Provider>
      </div>
    </main>
  );
}

export default App;
