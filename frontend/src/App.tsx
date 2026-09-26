import "./App.css";
import { Unity, useUnityContext } from "react-unity-webgl";

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
      <Unity className="unity-canvas" unityProvider={unityProvider} />
    </main>
  );
}

export default App;
