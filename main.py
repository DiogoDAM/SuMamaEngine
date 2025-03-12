import os

def replace_word_in_files(directory, target_word, replacement_word,
                          file_extension=".cs"):
    for root, _, files in os.walk(directory):
        for file in files:
            if file.endswith(file_extension):
                file_path = os.path.join(root, file)
                try:
                    with open(file_path, "r", encoding="utf-8") as f:
                        content = f.read()
                    
                    new_content = content.replace(target_word, replacement_word)
                    
                    with open(file_path, "w", encoding="utf-8") as f:
                        f.write(new_content)
                    
                    print(f"Modificado: {file_path}")
                except Exception as e:
                    print(f"Erro ao processar {file_path}: {e}")

# Exemplo de uso
if __name__ == "__main__":
    pasta = "./SuMamaLib"  # Altere para o caminho da pasta desejada
    palavra_antiga = "SuMamaLib"
    palavra_nova = "SuMamaEngine"
    replace_word_in_files(pasta, palavra_antiga, palavra_nova)
