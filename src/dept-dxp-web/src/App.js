import { BrowserRouter } from 'react-router-dom';
import useComposition from './hooks/useComposition';
import './App.css';

const App = () => {
  return (
    <BrowserRouter>
      <Main />
    </BrowserRouter>
  );
}

const Main = () => {

  const { composition = {} } = useComposition();
  const { name = "Loading..." } = composition;

  return (
    <div className="relative flex min-h-screen flex-col overflow-hidden bg-gray-50 py-6 sm:py-12">
      <div className="relative bg-white px-6 pt-10 pb-8 shadow-xl ring-1 ring-gray-900/5 w-11/12 mx-auto">
        <h1>{name}</h1>
      </div>
    </div>
  );
}

export default App;