import DynamicForm from "./DynamicForm";
import { domain } from "../utils";
import { FC, useState, useEffect } from "react";
import { initialStateMap } from "../config/initialStates";

type PostPartProps = {
  endpoint: string;
}

const PostPart: FC<PostPartProps> = ({ endpoint }) => {
  const [error, setError] = useState<string | null>(null);
  const [initState, setInitState] = useState<Record<string, unknown> | null>(null);

  const handleSuccess = (response: unknown) => {
    console.log("Úspěšně vytvořeno:", response);
  };

  useEffect(() => {
    try {
      const lastPart = endpoint.split('/').pop()?.replace(/s$/, '') || '';
      const availableTypes = Object.keys(initialStateMap).join(', ');
      
      const state = initialStateMap[lastPart];
      if (!state) {
        setError(`No initial state defined for type: ${lastPart}. Available types are: ${availableTypes}`);
        return;
      }

      setInitState(state);
      setError(null);
    } catch (e) {
      setError(e instanceof Error ? e.message : 'Failed to create initial state');
    }
  }, [endpoint]);

  if (error) {
    return (
      <div style={{ color: 'red', padding: '20px', border: '1px solid red' }}>
        <p>{error}</p>
      </div>
    );
  }

  if (!initState) {
    return null;
  }

  return (
    <div>
      <h2>Vytvořit záznam</h2>
      <DynamicForm
        endpoint={`${domain}${endpoint}`}
        initialState={initState}
        onSuccess={handleSuccess}
      />
    </div>
  );
};

export default PostPart;
