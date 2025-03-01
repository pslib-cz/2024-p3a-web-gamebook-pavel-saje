export async function postData<TInput, TOutput>(url: string, data: TInput): Promise<TOutput> {
    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
  
    if (!response.ok) {
      throw new Error(`Failed to create resource at ${url}`);
    }
  
    return response.json();
  }