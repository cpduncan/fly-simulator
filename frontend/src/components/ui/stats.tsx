// import {
//   Badge,
//   Stat as ChakraStat,
//   FormatNumber,
//   Show,
// } from "@chakra-ui/react";
// // import { InfoTip } from ".ui/toggle-tip"
// import * as React from "react";

// interface StatProps extends ChakraStat.RootProps {
//   label?: React.ReactNode;
//   value?: number;
//   info?: React.ReactNode;
//   valueText?: React.ReactNode;
//   formatOptions?: Intl.NumberFormatOptions;
//   change?: number;
// }

// export const Stat = React.forwardRef<HTMLDivElement, StatProps>(
//   function Stat(props, ref) {
//     const { label, value, valueText, change, info, formatOptions, ...rest } =
//       props;
//     return (
//       <ChakraStat.Root {...rest}>
//         {label && (
//           <ChakraStat.Label>
//             {label}
//             {/* {info && <InfoTip>{info}</InfoTip>} */}
//           </ChakraStat.Label>
//         )}
//         <ChakraStat.ValueText {...rest} ref={ref}>
//           {valueText ||
//             (value != null && formatOptions && (
//               <FormatNumber value={value} {...formatOptions} />
//             ))}
//         </ChakraStat.ValueText>
//         {change != null && (
//           <Badge colorPalette={change > 0 ? "green" : "red"} gap="0">
//             <Show when={change > 0} fallback={<ChakraStat.DownIndicator />}>
//               <ChakraStat.UpIndicator />
//             </Show>
//             <FormatNumber
//               value={Math.abs(change)}
//               style="percent"
//               maximumFractionDigits={2}
//             />
//           </Badge>
//         )}
//       </ChakraStat.Root>
//     );
//   },
// );

import { Badge, HStack, Stat } from "@chakra-ui/react";

export const Stat_CumulativeTotalFood = () => {
  return (
    <Stat.Root className="stat">
      <Stat.Label>Cumulative total food </Stat.Label>
      <HStack>
        <Stat.ValueText>3205 </Stat.ValueText>
      </HStack>
    </Stat.Root>
  );
};

export const Stat_GenTotalFood = () => {
  return (
    <Stat.Root className="stat-with-change">
      <Stat.Label>Total generation food </Stat.Label>
      <HStack>
        <Stat.ValueText>132</Stat.ValueText>
      </HStack>
    </Stat.Root>
  );
};

export const Stat_GenAvgFood = () => {
  return (
    <Stat.Root className="stat-with-change">
      <Stat.Label>Life-avg food </Stat.Label>
      <HStack>
        <Stat.ValueText>3.42 </Stat.ValueText>
        <Badge colorPalette="green" gap="0">
          <Stat.UpIndicator />
          2.4%
        </Badge>
      </HStack>
      <Stat.HelpText style={{ textAlign: "right" }}>
        since last generation
      </Stat.HelpText>
    </Stat.Root>
  );
};
