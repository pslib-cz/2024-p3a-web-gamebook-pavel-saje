import { useState } from "react";
import { postData } from "../api/post";

interface Props<T> {
  endpoint: string;
  initialState: T;
  onSuccess: (response: unknown) => void;
}

const DynamicForm = <T extends object>({ endpoint, initialState, onSuccess }: Props<T>) => {
  const [formData, setFormData] = useState<T>(initialState);
  const [message, setMessage] = useState<string | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await postData<T, unknown>(endpoint, formData);
      setMessage("Úspěšně odesláno!");
      onSuccess(response);
    } catch (error) {
      setMessage("Chyba při odesílání.");
      console.log(error)
    }
  };

  return (
    <div>
      <h3>{endpoint}</h3>
      <form onSubmit={handleSubmit}>
        {Object.keys(initialState).map((key) => (
          <input
            key={key as string}
            name={key as string}
            value={(formData as { [key: string]: string })[key] || ""}
            onChange={handleChange}
            placeholder={key as string}
            required
          />
        ))}
        <button type="submit">Odeslat</button>
      </form>
      {message && <p>{message}</p>}
    </div>
  );
};

export default DynamicForm;
