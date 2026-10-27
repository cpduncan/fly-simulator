import { Accordion, Span } from "@chakra-ui/react";
import "./accordion.css";

export const SensoryDocumentation = () => {
  const variant = "subtle";

  return (
    <div className="sensory-documentation">
      <Accordion.Root variant={variant} collapsible defaultValue={["b"]}>
        {items.map((item, index) => (
          <Accordion.Item
            className="accordion-item"
            key={index}
            value={item.value}
          >
            <Accordion.ItemTrigger>
              <Span flex="1">{item.title}</Span>
              <Accordion.ItemIndicator />
            </Accordion.ItemTrigger>
            <Accordion.ItemContent>
              <Accordion.ItemBody>{item.text}</Accordion.ItemBody>
            </Accordion.ItemContent>
          </Accordion.Item>
        ))}
      </Accordion.Root>
    </div>
  );
};

const items = [
  { value: "a", title: "First Item", text: "Some value 1..." },
  { value: "b", title: "Second Item", text: "Some value 2..." },
  { value: "c", title: "Third Item", text: "Some value 3..." },
  { value: "d", title: "Fourth Item", text: "Some value 4..." },
  { value: "e", title: "Fifth Item", text: "Some value 5..." },
  { value: "f", title: "Sixth Item", text: "Some value 6..." },
];
